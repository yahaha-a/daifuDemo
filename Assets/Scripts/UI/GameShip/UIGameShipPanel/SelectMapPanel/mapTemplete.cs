/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class mapTemplete : UIElement, IController
	{
		public string Name;

		private IUIGameShipPanelModel _uiGameShipPanelModel;
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			
			mapName.text = Name;
			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.CurrentSelectMapName.Value = Name;
			});
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