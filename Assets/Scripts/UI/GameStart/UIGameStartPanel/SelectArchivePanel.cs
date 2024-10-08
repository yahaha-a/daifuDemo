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
	public partial class SelectArchivePanel : UIElement, IController
	{
		private IGameGlobalModel _gameGlobalModel;

		private IArchiveSystem _archiveSystem;

		private List<GameObject> _archiveList = new List<GameObject>();

		private void Start()
		{
			_gameGlobalModel = this.GetModel<IGameGlobalModel>();

			_archiveSystem = this.GetSystem<IArchiveSystem>();

			Confirm.onClick.AddListener(() =>
			{
				if (_gameGlobalModel.CurrentSelectArchiveName.Value != null)
				{
					_gameGlobalModel.CurrentArchiveName.Value = _gameGlobalModel.CurrentSelectArchiveName.Value;
					_gameGlobalModel.CurrentSelectArchiveName.Value = null;
					Events.GameStart?.Trigger();
					SceneManager.LoadScene("GameShip");
				}
			});
			
			Cancel.onClick.AddListener(() =>
			{
				_gameGlobalModel.CurrentSelectArchiveName.Value = null;
				transform.gameObject.Hide();
			});
		}

		private void OnEnable()
		{
			var directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/GameArchive");
			string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

			if (jsonFiles.Length != 0)
			{
				foreach (string file in jsonFiles)
				{
					string fileName = Path.GetFileNameWithoutExtension(file);

					archiveTemplete.InstantiateWithParent(archieveRoot).Self(self =>
					{
						self.Name = fileName;
						self.Show();
						_archiveList.Add(self.gameObject);
					});
				}
			}
			
			this.GetUtility<IUtils>().AdjustContentHeight(archieveRoot);
		}

		private void OnDisable()
		{
			foreach (GameObject map in _archiveList)
			{
				map.DestroySelf();
			}
			_archiveList.Clear();
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