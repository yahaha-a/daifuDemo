using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:a4ebd5c4-c798-4952-85a1-607b12ccd52b
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button StartGameButton;
		[SerializeField]
		public UnityEngine.UI.Button SelectArchiveButton;
		[SerializeField]
		public UnityEngine.UI.Button CreateMapButton;
		[SerializeField]
		public SelectArchivePanel SelectArchivePanel;
		[SerializeField]
		public CreateArchivePanel CreateArchivePanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			StartGameButton = null;
			SelectArchiveButton = null;
			CreateMapButton = null;
			SelectArchivePanel = null;
			CreateArchivePanel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
