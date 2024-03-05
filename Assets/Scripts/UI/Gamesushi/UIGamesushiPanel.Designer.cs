using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:3392aada-6d63-410b-b382-9f76cd61885b
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
		
		private UIGamesushiPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UIAllTimePanel = null;
			UIsushiIngredientPanel = null;
			UIsushiMenuPanel = null;
			CustomerOrderPanel = null;
			StaffManagePanel = null;
			GoShipPanel = null;
			
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
