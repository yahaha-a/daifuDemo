using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:b44766a0-a1bf-4aaf-b706-a23ab50977a0
	public partial class UIGameOverPanel
	{
		public const string Name = "UIGameOverPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BackToStartPanelButton;
		
		private UIGameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BackToStartPanelButton = null;
			
			mData = null;
		}
		
		public UIGameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
