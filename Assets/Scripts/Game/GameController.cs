using System;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class GameController : ViewController, IController
	{
		private IPlayerModel PlayerModel;
		private void Start()
		{
			PlayerModel = this.GetModel<IPlayerModel>();

			UIKit.OpenPanel<UIGamePanel>();

			PlayerModel.NumberOfFish.Register(number =>
			{
				if (number == 5)
				{
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			PlayerModel.PlayerOxygen.Register(oxygen =>
			{
				if (oxygen <= 0)
				{
					GameObject.FindGameObjectWithTag("Player").DestroySelf();
					UIKit.OpenPanel<UIGameOverPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
