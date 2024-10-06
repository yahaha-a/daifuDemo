/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class archiveTemplete : UIElement, IController
	{
		public string Name;

		private IGameGlobalModel _gameGlobalModel;
		
		private void Awake()
		{
			_gameGlobalModel = this.GetModel<IGameGlobalModel>();
			
			mapName.text = Name;
			
			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				_gameGlobalModel.CurrentSelectArchiveName.Value = Name;
			});
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