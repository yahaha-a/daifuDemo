using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:df717f20-2912-4282-94ed-e8475bd1e50b
	public partial class UIGamesushiPanel
	{
		public const string Name = "UIGamesushiPanel";
		
		[SerializeField]
		public UIsushiIngredientPanelManage UIsushiIngredientPanel;
		[SerializeField]
		public UIsushiMenuPanel UIsushiMenuPanel;
		[SerializeField]
		public CustomerOrderPanel CustomerOrderPanel;
		[SerializeField]
		public StaffManagePanel StaffManagePanel;
		
		private UIGamesushiPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UIsushiIngredientPanel = null;
			UIsushiMenuPanel = null;
			CustomerOrderPanel = null;
			StaffManagePanel = null;
			
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
