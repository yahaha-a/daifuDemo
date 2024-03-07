using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:3d098c79-bb95-452c-8202-580034e4ea77
	public partial class UIGamesushiPanel
	{
		public const string Name = "UIGamesushiPanel";
		
		[SerializeField]
		public UIAllTimePanel UIAllTimePanel;
		[SerializeField]
		public UIsushiIngredientPanelManage UIsushiIngredientPanel;
		[SerializeField]
		public UIsushiMenuPanel UIsushiMenuPanel;
		[SerializeField]
		public CustomerOrderPanel CustomerOrderPanel;
		[SerializeField]
		public StaffManagePanel StaffManagePanel;
		[SerializeField]
		public GoShipPanel GoShipPanel;
		[SerializeField]
		public UINewMenuUnLockPanel UINewMenuUnLockPanel;
		
		private UIGamesushiPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UIAllTimePanel = null;
			UIsushiIngredientPanel = null;
			UIsushiMenuPanel = null;
			CustomerOrderPanel = null;
			StaffManagePanel = null;
			GoShipPanel = null;
			UINewMenuUnLockPanel = null;
			
			mData = null;
		}
		
		public UIGamesushiPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamesushiPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamesushiPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
