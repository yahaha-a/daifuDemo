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

			PlayerModel.NumberOfFish.Register(number =>
			{
				if (number == 1)
				{
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
