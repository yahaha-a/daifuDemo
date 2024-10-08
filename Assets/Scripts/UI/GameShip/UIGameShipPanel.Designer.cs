using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:10c92697-ea86-481c-bab3-7e71a76b57b8
	public partial class UIGameShipPanel
	{
		public const string Name = "UIGameShipPanel";
		
		[SerializeField]
		public GoToHomePanel GoToHomePanel;
		[SerializeField]
		public GoToSeaPanel GoToSeaPanel;
		[SerializeField]
		public UIShipUIPackPanelManage UIShipUIPackPanel;
		[SerializeField]
		public SelectMapPanel SelectMapPanel;
		
		private UIGameShipPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			GoToHomePanel = null;
			GoToSeaPanel = null;
			UIShipUIPackPanel = null;
			SelectMapPanel = null;
			
			mData = null;
		}
		
		public UIGameShipPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameShipPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameShipPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
