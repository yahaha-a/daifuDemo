using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:c9f5677f-dde4-419d-b6b0-ea7639e2df1a
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text NumberOfFish;
		[SerializeField]
		public UnityEngine.UI.Image Oxygen;
		[SerializeField]
		public UnityEngine.UI.Text OxygenValue;
		[SerializeField]
		public UnityEngine.UI.Button BackPackButton;
		[SerializeField]
		public UnityEngine.UI.Button CloseButton;
		[SerializeField]
		public WeaponPanel WeaponPanel;
		[SerializeField]
		public UIPackPanel UIPackPanel;
		[SerializeField]
		public UISettlePanel UISettlePanel;
		[SerializeField]
		public UIHarvestPanel UIHarvestPanel;
		[SerializeField]
		public CounterPanel CounterPanel;
		[SerializeField]
		public WeaponUpgradePanel WeaponUpgradePanel;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			NumberOfFish = null;
			Oxygen = null;
			OxygenValue = null;
			BackPackButton = null;
			CloseButton = null;
			WeaponPanel = null;
			UIPackPanel = null;
			UISettlePanel = null;
			UIHarvestPanel = null;
			CounterPanel = null;
			WeaponUpgradePanel = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
