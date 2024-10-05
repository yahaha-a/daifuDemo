using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace MapEditor
{
	public class UIMapEditorData : UIPanelData
	{
	}
	public partial class UIMapEditor : UIPanel, IController
	{
		private IMapEditorSystem _mapEditorSystem;
		private IMapEditorModel _mapEditorModel;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIMapEditorData ?? new UIMapEditorData();

			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();
			_mapEditorModel = this.GetModel<IMapEditorModel>();

			this.GetModel<IMapEditorModel>().CurrentArchiveName.Register(name =>
			{
				CurrentArchiveName.text = name;
				CurrentArchiveName.Show();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			NewButton.onClick.AddListener(() =>
			{
				NewMapPanel.Show();
			});
			
			ReadButton.onClick.AddListener(() =>
			{
				ReadMapPanel.Show();
			});
			
			SaveButton.onClick.AddListener(() =>
			{
				SaveMapPanel.Show();
			});
			
			QuitButton.onClick.AddListener(() =>
			{
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
			return MapEditorGlobal.Interface;
		}
	}
}
