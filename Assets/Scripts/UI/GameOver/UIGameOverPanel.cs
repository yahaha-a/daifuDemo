using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public class UIGameOverPanelData : UIPanelData
	{
	}
	public partial class UIGameOverPanel : UIPanel, IController
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameOverPanelData ?? new UIGameOverPanelData();
			
			BackToStartPanelButton.onClick.AddListener(() =>
			{
				this.SendCommand<ReloadDataCommand>();
				SceneManager.LoadScene("GameStart");
				this.CloseSelf();
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

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
