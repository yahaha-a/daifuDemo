using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class Gun : ViewController, IController
	{
		public float rotationRate;

		public float intervalBetweenShots;

		public float currentIntervalBetweenShots;

		public List<(Vector2, float)> BulletSpawnLocationsAndDirectionsList;

		public bool ifLeft = false;

		public GameObject flyerRoot;
		
		private IGunModel _gunModel;

		private IPlayerModel _playerModel;

		private GunFsm _gunFsm;

		private void Start()
		{
			_gunFsm = new GunFsm(this, BulletTemplate);
			
			_gunModel = this.GetModel<IGunModel>();
			
			_playerModel = this.GetModel<IPlayerModel>();

			_gunModel.CurrentGunKey.RegisterWithInitValue(key =>
			{
				UpdateData();
			});
			
			_gunModel.CurrentGunRank.RegisterWithInitValue(rank =>
			{
				UpdateData();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				ifLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			_gunFsm.Tick();
		}

		public void UpdateData()
		{
			rotationRate =
				this.SendQuery(new FindGunRotationRate(_gunModel.CurrentGunKey.Value, _gunModel.CurrentGunRank.Value));
			intervalBetweenShots =
				this.SendQuery(new FindGunIntervalBetweenShots(_gunModel.CurrentGunKey.Value,
					_gunModel.CurrentGunRank.Value));
			BulletSpawnLocationsAndDirectionsList = this.SendQuery(
				new FindBulletSpawnLocationsAndDirectionsList(_gunModel.CurrentGunKey.Value,
					_gunModel.CurrentGunRank.Value));
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
