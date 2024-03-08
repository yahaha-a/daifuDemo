using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:76a78ea3-fd6a-4dff-a280-0470fbbdcb9b
	public partial class UIGameGlobalPanel
	{
		public const string Name = "UIGameGlobalPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button OpenTaskSelectButton;
		[SerializeField]
		public UITaskDisplayPanel UITaskDisplayPanel;
		[SerializeField]
		public UITaskSelectPanel UITaskSelectPanel;
		
		private UIGameGlobalPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			OpenTaskSelectButton = null;
			UITaskDisplayPanel = null;
			UITaskSelectPanel = null;
			
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
