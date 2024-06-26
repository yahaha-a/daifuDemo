using System;
using UnityEngine;
using QFramework;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class FishFork : ViewController, IController
	{
		private bool _ifLeft;

		private float _rotationRate;

		private GameObject _flyerRoot;

		private IPlayerModel _playerModel;

		private IFishForkModel _fishForkModel;

		private IFishForkHeadModel _fishForkHeadModel;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();

			_fishForkModel = this.GetModel<IFishForkModel>();

			_fishForkHeadModel = this.GetModel<IFishForkHeadModel>();
			
			_flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");
			
			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				_ifLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CatchFish.Register(fish =>
			{
				_fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
				_fishForkModel.FishForkIfShooting.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FishEscape.Register(fish =>
			{
				_fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
				_fishForkModel.FishForkIfShooting.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_fishForkModel.CurrentFishForkKey.RegisterWithInitValue(key =>
			{
				UpdateData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_fishForkModel.CurrentRank.RegisterWithInitValue(rank =>
			{
				UpdateData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			TransitionState();
			TakeAction();
		}

		private void TransitionState()
		{
			switch (_fishForkModel.CurrentFishForkState.Value)
			{
				case FishForkState.Ready:
					if (Input.GetKeyDown(KeyCode.I))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Aim;
					}
					break;
				case FishForkState.Aim:
					if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Revolve;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
					}
					else if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Launch;
					}
					break;
				case FishForkState.Revolve:
					if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Launch;
					}
					else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Aim;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
					}
					break;
				case FishForkState.Launch:
					_fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
					break;
			}
		}

		private void TakeAction()
		{
			switch (_fishForkModel.CurrentFishForkState.Value)
			{
				case FishForkState.Ready:
					break;
				case FishForkState.Aim:
					Events.FishForkIsNotUse?.Trigger(false);
					break;
				case FishForkState.Revolve:
					Events.FishForkIsNotUse?.Trigger(false);
					
					if (Input.GetKey(KeyCode.Z))
					{
						if (transform.eulerAngles.z < 70f || transform.eulerAngles.z > 289f)
						{
							var rotationAmount = _rotationRate * Time.deltaTime;
							transform.Rotate(new Vector3(0, 0, rotationAmount));
						}
					}
					else if (Input.GetKey(KeyCode.C))
					{
						if (transform.eulerAngles.z < 71f || transform.eulerAngles.z > 290f)
						{
							var rotationAmount = -_rotationRate * Time.deltaTime;
							transform.Rotate(new Vector3(0, 0, rotationAmount));
						}
					}
					break;
				case FishForkState.Launch:
					Events.FishForkIsNotUse?.Trigger(false);
					
					if (!_fishForkModel.FishForkIfShooting.Value)
					{
						FishForkHeadTemplate.InstantiateWithParent(this)
							.Self(self =>
							{
								if (_ifLeft)
								{
									_fishForkHeadModel.FishForkHeadDirection = -1;
								}
								else
								{
									_fishForkHeadModel.FishForkHeadDirection = 1;
								}
								self.parent = _flyerRoot.transform;
								self.Show();
							});
						_fishForkModel.FishForkIfShooting.Value = true;
					}
					break;
			}
		}

		private void UpdateData()
		{
			_rotationRate = this.SendQuery(new FindFishForkRotationRate(_fishForkModel.CurrentFishForkKey.Value,
				_fishForkModel.CurrentRank.Value));
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
