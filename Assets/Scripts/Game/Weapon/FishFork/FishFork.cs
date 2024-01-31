using System;
using UnityEngine;
using QFramework;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public enum FishForkState
	{
		Ready,
		Aim,
		Revolve,
		Launch
	}
	
	public partial class FishFork : ViewController, Iweapon
	{
		public string Key { get; } = Config.FishForkKey;
		
		private float _rotationRate = 50f;

		private FishForkState _fishForkState = FishForkState.Ready;

		private bool _ifLeft = false;

		private GameObject FlyerRoot;

		public bool FishForkIfShooting = false;

		private void Start()
		{
			FlyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");
			
			FishForkHead.FishForkHeadDestroy.Register(() =>
			{
				FishForkIfShooting = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.PlayerVeer.Register(value =>
			{
				_ifLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			TransitionState();
			TakeAction();
		}

		private void TransitionState()
		{
			switch (_fishForkState)
			{
				case FishForkState.Ready:
					if (Input.GetKeyDown(KeyCode.I))
					{
						_fishForkState = FishForkState.Aim;
					}
					break;
				case FishForkState.Aim:
					if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
					{
						_fishForkState = FishForkState.Revolve;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkState = FishForkState.Ready;
					}
					else if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkState = FishForkState.Launch;
					}
					break;
				case FishForkState.Revolve:
					if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkState = FishForkState.Launch;
					}
					else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
					{
						_fishForkState = FishForkState.Aim;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkState = FishForkState.Ready;
					}
					break;
				case FishForkState.Launch:
					_fishForkState = FishForkState.Ready;
					break;
			}
		}

		private void TakeAction()
		{
			switch (_fishForkState)
			{
				case FishForkState.Ready:
					if (FishForkIfShooting == false)
					{
						Events.FishForkIsNotUse?.Trigger(true);
					}
					else
					{
						Events.FishForkIsNotUse?.Trigger(false);
					}
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
					
					if (!FishForkIfShooting)
					{
						FishForkHeadTemplate.InstantiateWithParent(this)
							.Self(self =>
							{
								if (_ifLeft)
								{
									self.GetComponent<FishForkHead>().Direction = -1;
								}
								else
								{
									self.GetComponent<FishForkHead>().Direction = 1;
								}
								self.parent = FlyerRoot.transform;
								self.Show();
							});
						FishForkIfShooting = true;
					}
					break;
			}
		}

	}
}
