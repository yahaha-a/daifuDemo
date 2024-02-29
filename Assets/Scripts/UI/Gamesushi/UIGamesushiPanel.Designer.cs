using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:20ab2f99-295e-473b-88f5-ac661a5bb648
	public partial class UIGamesushiPanel
	{
		public const string Name = "UIGamesushiPanel";
		
		[SerializeField]
		public UIsushiIngredientPanelManage UIsushiIngredientPanel;
		[SerializeField]
		public UIsushiMenuPanel UIsushiMenuPanel;
		[SerializeField]
		public CustomerOrderPanel CustomerOrderPanel;
		
		private UIGamesushiPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UIsushiIngredientPanel = null;
			UIsushiMenuPanel = null;
			CustomerOrderPanel = null;
			
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
