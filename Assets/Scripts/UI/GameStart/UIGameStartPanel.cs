using System.IO;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel, IController
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();

			StartGameButton.onClick.AddListener(() =>
			{
				CreateArchivePanel.Show();
			});
			
			SelectArchiveButton.onClick.AddListener((() =>
			{
				SelectArchivePanel.Show();
			}));
			
			CreateMapButton.onClick.AddListener(() =>
			{
				SceneManager.LoadScene("MapEditor");
				this.CloseSelf();
			});

			Events.GameStart.Register(() =>
			{
				this.CloseSelf();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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
