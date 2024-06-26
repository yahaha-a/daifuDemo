/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.TestTools;

namespace daifuDemo
{
	public partial class CatchFishPanel : UIElement, IController
	{
		private IPlayerModel _playerModel;
		private IUIGamePanelModel _uiGamePanelModel;
		
		private void Awake()
		{
			_playerModel = this.GetModel<IPlayerModel>();
			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();


			_playerModel.FishingChallengeClicks.Register(click =>
			{
				if (_playerModel.MaxFishingChallengeClicks.Value != 0)
				{
					float result = (float)click / _playerModel.MaxFishingChallengeClicks.Value;
					ProgressBar.value = result;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Events.FishEscape.Register(fish =>
			{
				_uiGamePanelModel.IfCatchFishPanelShow.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Events.CatchFish.Register(fish =>
			{
				_uiGamePanelModel.IfCatchFishPanelShow.Value = false;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}