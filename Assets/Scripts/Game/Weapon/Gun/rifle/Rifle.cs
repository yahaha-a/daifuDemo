using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Rifle : ViewController, IController, Iweapon
	{
		public string Key { get; } = Config.RifleKey;
		
		public GunState _rifleState = GunState.Ready;
		
		private float _rotationRate = 100f;

		private float _intervalBetweenShots = 0.2f;

		private bool _ifLeft = false;

		private GameObject FlyerRoot;

		private IPlayerModel _playerModel;

		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();
			
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
			switch (_rifleState)
			{
				case GunState.Ready:
					if (Input.GetKeyDown(KeyCode.I))
					{
						_rifleState = GunState.Aim;
					}
					break;
				case GunState.Aim:
					if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
					{
						_rifleState = GunState.Revolve;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_rifleState = GunState.Ready;
					}
					else if (Input.GetKeyDown(KeyCode.J))
					{
						_rifleState = GunState.Shooting;
					}
					break;
				case GunState.Revolve:
					if (Input.GetKeyDown(KeyCode.J))
					{
						_rifleState = GunState.Shooting;
					}
					else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
					{
						_rifleState = GunState.Aim;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_rifleState = GunState.Ready;
					}
					break;
				case GunState.Shooting:
					_rifleState = GunState.Cooling;
					break;
			}
		}

		private void TakeAction()
		{
			switch (_rifleState)
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
						_rifleState = GunState.Ready;
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
