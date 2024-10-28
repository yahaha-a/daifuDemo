using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:a8c5c3c7-0223-4bec-8e23-0bc182737f28
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
		public UIPackPanel UIPackPanel;
		[SerializeField]
		public UISettlePanel UISettlePanel;
		[SerializeField]
		public UIHarvestPanel UIHarvestPanel;
		[SerializeField]
		public CatchFishPanel CatchFishPanel;
		[SerializeField]
		public WeaponPanel WeaponPanel;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			NumberOfFish = null;
			Oxygen = null;
			OxygenValue = null;
			BackPackButton = null;
			CloseButton = null;
			UIPackPanel = null;
			UISettlePanel = null;
			UIHarvestPanel = null;
			CatchFishPanel = null;
			WeaponPanel = null;
			
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
