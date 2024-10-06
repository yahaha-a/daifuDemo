using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:478cdf66-8a82-476b-811a-ea3c8022f5a3
	public partial class UIGameGlobalPanel
	{
		public const string Name = "UIGameGlobalPanel";
		
		[SerializeField]
		public RectTransform UITaskPanel;
		[SerializeField]
		public UnityEngine.UI.Button OpenTaskSelectButton;
		[SerializeField]
		public UITaskDisplayPanel UITaskDisplayPanel;
		[SerializeField]
		public UITaskSelectPanel UITaskSelectPanel;
		[SerializeField]
		public UnityEngine.UI.Button TaskShowButton;
		[SerializeField]
		public UnityEngine.UI.Text TaskShowButtonName;
		[SerializeField]
		public ObtainItemsPanel ObtainItemsPanel;
		
		private UIGameGlobalPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UITaskPanel = null;
			OpenTaskSelectButton = null;
			UITaskDisplayPanel = null;
			UITaskSelectPanel = null;
			TaskShowButton = null;
			TaskShowButtonName = null;
			ObtainItemsPanel = null;
			
			mData = null;
		}
		
		public UIGameGlobalPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameGlobalPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameGlobalPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
