using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Gun : ViewController, IController, Iweapon
	{
		public string Key { get; private set; } = Config.RifleKey;

		private float _rotationRate;

		private float _intervalBetweenShots;

		private bool _ifLeft = false;

		private GameObject FlyerRoot;
		
		private IGunModel _gunModel;

		private IPlayerModel _playerModel;

		private void Start()
		{
			_gunModel = this.GetModel<IGunModel>();
			
			_playerModel = this.GetModel<IPlayerModel>();

			_gunModel.CurrentGunKey.RegisterWithInitValue(key =>
			{
				Key = key;
			});
			
			_gunModel.CurrentGunRank.RegisterWithInitValue(rank =>
			{
				_rotationRate = this.SendQuery(new FindGunRotationRate(Key, rank));
				_intervalBetweenShots = this.SendQuery(new FindGunIntervalBetweenShots(Key, rank));
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			FlyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");

			_playerModel.IfLeft.RegisterWithInitValue(value =>
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
			switch (_gunModel.CurrentGunState.Value)
			{
				case GunState.Ready:
					if (Input.GetKeyDown(KeyCode.I))
					{
						_gunModel.CurrentGunState.Value = GunState.Aim;
					}
					break;
				case GunState.Aim:
					if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
					{
						_gunModel.CurrentGunState.Value = GunState.Revolve;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_gunModel.CurrentGunState.Value = GunState.Ready;
					}
					else if (Input.GetKeyDown(KeyCode.J))
					{
						_gunModel.CurrentGunState.Value = GunState.Shooting;
					}
					break;
				case GunState.Revolve:
					if (Input.GetKeyDown(KeyCode.J))
					{
						_gunModel.CurrentGunState.Value = GunState.Shooting;
					}
					else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
					{
						_gunModel.CurrentGunState.Value = GunState.Aim;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_gunModel.CurrentGunState.Value = GunState.Ready;
					}
					break;
				case GunState.Shooting:
					_gunModel.CurrentGunState.Value = GunState.Cooling;
					break;
			}
		}

		private void TakeAction()
		{
			switch (_gunModel.CurrentGunState.Value)
			{
				case GunState.Ready:
					break;
				case GunState.Aim:
					break;
				case GunState.Revolve:
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
				case GunState.Shooting:
					BulletTemplate.InstantiateWithParent(this)
						.Self(self =>
						{
							if (_ifLeft)
							{
								self.GetComponent<Bullet>().Direction = -1;
							}
							else
							{
								self.GetComponent<Bullet>().Direction = 1;
							}
							self.parent = FlyerRoot.transform;
							self.Show();
						});
					break;
				case GunState.Cooling:
					ActionKit.Delay(_intervalBetweenShots, () =>
					{
						_gunModel.CurrentGunState.Value = GunState.Ready;
					}).Start(this);
					break;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
