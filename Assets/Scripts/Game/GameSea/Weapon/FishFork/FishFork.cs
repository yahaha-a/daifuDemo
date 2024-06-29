using System;
using UnityEngine;
using QFramework;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public partial class FishFork : ViewController, IController
	{
		public bool ifLeft;

		public float rotationRate;

		private IPlayerModel _playerModel;

		private IFishForkModel _fishForkModel;

		private FishForkFsm _fishForkFsm;

		private void Start()
		{
			_fishForkFsm = new FishForkFsm(this, FishForkHeadTemplate);
			
			_playerModel = this.GetModel<IPlayerModel>();

			_fishForkModel = this.GetModel<IFishForkModel>();

			_playerModel.IfLeft.RegisterWithInitValue(value =>
			{
				ifLeft = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.FishForkHeadDestroy.Register(() =>
			{
				_fishForkModel.IfFishForkHeadExist.Value = false;
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
			_fishForkFsm.Tick();
		}

		private void UpdateData()
		{
			rotationRate = this.SendQuery(new FindFishForkRotationRate(_fishForkModel.CurrentFishForkKey.Value,
				_fishForkModel.CurrentRank.Value));
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
