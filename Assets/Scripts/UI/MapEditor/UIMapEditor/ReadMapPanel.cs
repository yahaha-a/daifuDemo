/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.IO;
using System.Collections.Generic;
using daifuDemo;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class ReadMapPanel : UIElement, IController
	{
		private List<GameObject> _archiveList = new List<GameObject>();

		private IMapEditorModel _mapEditorModel;

		private IMapEditorSystem _mapEditorSystem;

		private void Start()
		{
			_mapEditorModel = this.GetModel<IMapEditorModel>();
			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();
			
			Confirm.onClick.AddListener(() =>
			{
				_mapEditorModel.CurrentArchiveName.Value = _mapEditorModel.CurrentSelectArchiveName.Value;

				this.gameObject.Hide();
			});
			
			Cancel.onClick.AddListener(() =>
			{
				this.gameObject.Hide();
			});
		}

		private void OnEnable()
		{
			string directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");
			string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml");

			if (xmlFiles.Length != 0)
			{
				foreach (string file in xmlFiles)
				{
					string fileName = Path.GetFileNameWithoutExtension(file);

					archieveTemplete.InstantiateWithParent(archieveRoot).Self(self =>
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
			foreach (GameObject archiveItem in _archiveList)
			{
				archiveItem.DestroySelf();
			}
			_archiveList.Clear();
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return MapEditorGlobal.Interface;
		}
	}
}