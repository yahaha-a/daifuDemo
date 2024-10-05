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
	public partial class mapTemplete : UIElement, IController
	{
		public string Name;

		private IGameStartModel _gameStartModel;
		
		private void Awake()
		{
		}

		private void Start()
		{
			_gameStartModel = this.GetModel<IGameStartModel>();

			mapName.text = Name;
			
			this.GetComponent<Button>().onClick.AddListener((() =>
			{
				_gameStartModel.CurrentSelectMapName.Value = Name;
			}));
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