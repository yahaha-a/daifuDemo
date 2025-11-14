using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	// Generate Id:ebb863ba-4f2a-42aa-b8c1-3df0658326d3
	public partial class UIGameShipPanel
	{
		public const string Name = "UIGameShipPanel";
		
		[SerializeField]
		public GoToHomePanel GoToHomePanel;
		[SerializeField]
		public GoToSeaPanel GoToSeaPanel;
		[SerializeField]
		public UIShipUIPackPanelManage UIShipUIPackPanel;
		[SerializeField]
		public SelectMapPanel SelectMapPanel;
		[SerializeField]
		public EquipWeaponPanel EquipWeaponPanel;
		
		private UIGameShipPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			GoToHomePanel = null;
			GoToSeaPanel = null;
			UIShipUIPackPanel = null;
			SelectMapPanel = null;
			EquipWeaponPanel = null;
			
			mData = null;
		}
		
		public UIGameShipPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameShipPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameShipPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
