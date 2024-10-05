/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using MapEditor;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class SelectMapPanel : UIElement, IController
	{
		private IGameStartModel _gameStartModel;

		private IMapCreateSystem _mapCreateSystem;

		private List<GameObject> _mapList = new List<GameObject>();

		private void Start()
		{
			_gameStartModel = this.GetModel<IGameStartModel>();

			_mapCreateSystem = this.GetSystem<IMapCreateSystem>();

			Confirm.onClick.AddListener(() =>
			{
				if (_gameStartModel.CurrentSelectMapName.Value != null)
				{
					_mapCreateSystem.LoadItemsFromXmlFile(_gameStartModel.CurrentSelectMapName.Value);
					Events.GameStart?.Trigger();
					SceneManager.LoadScene("GameShip");
				}
				_gameStartModel.CurrentSelectMapName.Value = null;
			});
			
			Cancel.onClick.AddListener(() =>
			{
				_gameStartModel.CurrentSelectMapName.Value = null;
				transform.gameObject.Hide();
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

					mapTemplete.InstantiateWithParent(archieveRoot).Self(self =>
					{
						self.Name = fileName;
						self.Show();
						_mapList.Add(self.gameObject);
					});
				}
			}
			
			this.GetUtility<IUtils>().AdjustContentHeight(archieveRoot);
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