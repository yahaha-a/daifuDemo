using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	// Generate Id:022cb25c-6bc3-4f69-a6bd-4d04ad318f3a
	public partial class UIMapEditor
	{
		public const string Name = "UIMapEditor";
		
		[SerializeField]
		public RectTransform HightLightCursor;
		[SerializeField]
		public UnityEngine.UI.Image HightLightCursorIcon;
		[SerializeField]
		public SelectOptionsPanel SelectOptionsPanel;
		[SerializeField]
		public UnityEngine.UI.Button NewButton;
		[SerializeField]
		public UnityEngine.UI.Button ReadButton;
		[SerializeField]
		public UnityEngine.UI.Button SaveButton;
		[SerializeField]
		public UnityEngine.UI.Button QuitButton;
		[SerializeField]
		public NewMapPanel NewMapPanel;
		[SerializeField]
		public ReadMapPanel ReadMapPanel;
		[SerializeField]
		public SaveMapPanel SaveMapPanel;
		[SerializeField]
		public UnityEngine.UI.Text CurrentArchiveName;
		
		private UIMapEditorData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			HightLightCursor = null;
			HightLightCursorIcon = null;
			SelectOptionsPanel = null;
			NewButton = null;
			ReadButton = null;
			SaveButton = null;
			QuitButton = null;
			NewMapPanel = null;
			ReadMapPanel = null;
			SaveMapPanel = null;
			CurrentArchiveName = null;
			
			mData = null;
		}
		
		public UIMapEditorData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIMapEditorData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIMapEditorData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
