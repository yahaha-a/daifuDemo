using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Ship : ViewController, IController
	{
		private void Start()
		{
			var uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			
			TransportHome.OnTriggerEnter2DEvent(collider2d =>
			{
				if (collider2d.CompareTag("Player"))
				{
					uiGameShipPanelModel.IfGoToHomePanelOpen.Value = true;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			TransportHome.OnTriggerExit2DEvent(collider2d =>
			{
				if (collider2d.CompareTag("Player"))
				{
					uiGameShipPanelModel.IfGoToHomePanelOpen.Value = false;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			TransportSea.OnTriggerEnter2DEvent(collider2d =>
			{
				if (collider2d.CompareTag("Player"))
				{
					uiGameShipPanelModel.IfGotoSeaPanelOpen.Value = true;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			TransportHome.OnTriggerExit2DEvent(collider2d =>
			{
				if (collider2d.CompareTag("Player"))
				{
					uiGameShipPanelModel.IfGotoSeaPanelOpen.Value = false;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
