using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:7f8d43e9-6e67-44eb-bbf2-7e84b88bdf59
	public partial class UIGameShipPanel
	{
		public const string Name = "UIGameShipPanel";
		
		[SerializeField]
		public GoToHomePanel GoToHomePanel;
		[SerializeField]
		public GoToSeaPanel GoToSeaPanel;
		
		private UIGameShipPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			GoToHomePanel = null;
			GoToSeaPanel = null;
			
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
