/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CounterPanel : UIElement,IController
	{
		private IUIGamePanelModel _uiGamePanelModel;
		
		private void Awake()
		{
			_uiGamePanelModel = this.GetModel<IUIGamePanelModel>();
			
			_uiGamePanelModel.CurrentCounterPanelState.Register(value =>
			{
				switch (value)
				{
					case CounterPanelState.CatchFish:
						title.text = "捕鱼";
						break;
					case CounterPanelState.OpenTreasure:
						title.text = "开启";
						break;
					case CounterPanelState.Reloading:
						title.text = "换弹";
						break;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			_uiGamePanelModel.CurrentCounter.Register(value =>
			{
				ProgressBar.value = value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}