/****************************************************************************
 * 2024.9 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class OptionTemplete : UIElement, IController
	{
		public IMapEditorInfo MapEditorInfo;

		private IMapEditorModel _mapEditorModel;
		
		private void Start()
		{
			_mapEditorModel = this.GetModel<IMapEditorModel>();
			
			Name.text = MapEditorInfo.Name;
			
			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				_mapEditorModel.CurrentMapEditorName.Value = MapEditorInfo.Key;
			});
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