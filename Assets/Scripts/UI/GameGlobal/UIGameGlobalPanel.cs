using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public class UIGameGlobalPanelData : UIPanelData
	{
	}
	public partial class UIGameGlobalPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameGlobalPanelData ?? new UIGameGlobalPanelData();
			// please add init code here
			
			OpenTaskSelectButton.onClick.AddListener(() =>
			{
				UITaskSelectPanel.Show();
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
