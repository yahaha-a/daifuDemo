using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:11950e8f-29a5-4bb5-bc00-f5d184a879a2
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Oxygen;
		[SerializeField]
		public UnityEngine.UI.Text OxygenValue;
		[SerializeField]
		public UnityEngine.UI.Text NumberOfFish;
		[SerializeField]
		public UIPackPanel UIPackPanel;
		[SerializeField]
		public UISettlePanel UISettlePanel;
		[SerializeField]
		public UIHarvestPanel UIHarvestPanel;
		[SerializeField]
		public CatchFishPanel CatchFishPanel;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Oxygen = null;
			OxygenValue = null;
			NumberOfFish = null;
			UIPackPanel = null;
			UISettlePanel = null;
			UIHarvestPanel = null;
			CatchFishPanel = null;
			
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
