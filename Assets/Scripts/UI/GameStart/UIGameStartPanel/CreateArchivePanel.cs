/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
	public partial class CreateArchivePanel : UIElement, IController
	{
		private IArchiveSystem _archiveSystem;

		private IGameGlobalModel _gameGlobalModel;
		
		private void Start()
		{
			_archiveSystem = this.GetSystem<IArchiveSystem>();
			_gameGlobalModel = this.GetModel<IGameGlobalModel>();
			
			Confirm.onClick.AddListener(() =>
			{
				string archiveName = ArchiveName.text;
				if (archiveName != null)
				{
					_archiveSystem.CreateEmptyArchive(archiveName);
					_gameGlobalModel.CurrentArchiveName.Value = archiveName;
					Events.GameStart?.Trigger();
					SceneManager.LoadScene("GameShip");
				}
			});
			
			Cancel.onClick.AddListener(() =>
			{
				ArchiveName.text = null;
				transform.gameObject.Hide();
			});
		}

		private void OnEnable()
		{
			
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