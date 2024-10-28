/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class SelectMapPanel : UIElement, IController
	{
		private IUIGameShipPanelModel _uiGameShipPanelModel;

		private IMapCreateSystem _mapCreateSystem;
		
		private List<GameObject> _mapList = new List<GameObject>();
		
		private void Awake()
		{
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			_mapCreateSystem = this.GetSystem<IMapCreateSystem>();
			
			Confirm.onClick.AddListener(() =>
			{
				if (_uiGameShipPanelModel.CurrentSelectMapName.Value != null)
				{
					_uiGameShipPanelModel.CurrentMapName.Value = _uiGameShipPanelModel.CurrentSelectMapName.Value;
					_uiGameShipPanelModel.CurrentSelectMapName.Value = null;
					
					_mapCreateSystem.LoadItemsFromXmlFile(_uiGameShipPanelModel.CurrentMapName.Value);

					_uiGameShipPanelModel.IfSelectMapPanelShow.Value = false;
					SceneManager.LoadScene("Game");
				}
			});
			
			Cancel.onClick.AddListener(() =>
			{
				_uiGameShipPanelModel.CurrentSelectMapName.Value = null;
				_uiGameShipPanelModel.CurrentMapName.Value = null;
				_uiGameShipPanelModel.IfSelectMapPanelShow.Value = false;
			});
		}

		private void OnEnable()
		{
			var directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");
			string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml");

			if (xmlFiles.Length != 0)
			{
				foreach (string file in xmlFiles)
				{
					string fileName = Path.GetFileNameWithoutExtension(file);

					mapTemplete.InstantiateWithParent(mapRoot).Self(self =>
					{
						self.Name = fileName;
						self.Show();
						_mapList.Add(self.gameObject);
					});
				}
			}
			
			this.GetUtility<IUtils>().AdjustContentHeight(mapRoot);
		}

		private void OnDisable()
		{
			foreach (GameObject map in _mapList)
			{
				map.DestroySelf();
			}
			_mapList.Clear();
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}