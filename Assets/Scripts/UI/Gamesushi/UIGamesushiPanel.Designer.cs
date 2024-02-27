using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:ecc5122d-1d57-4a88-a9e7-ac32fde26283
	public partial class UIGamesushiPanel
	{
		public const string Name = "UIGamesushiPanel";
		
		[SerializeField]
		public UIsushiIngredientPanelManage UIsushiIngredientPanel;
		[SerializeField]
		public UIsushiMenuPanel UIsushiMenuPanel;
		
		private UIGamesushiPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UIsushiIngredientPanel = null;
			UIsushiMenuPanel = null;
			
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
