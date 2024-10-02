/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class SaveMapPanel : UIElement, IController
	{
		private IMapEditorModel _mapEditorModel;
		private IMapEditorSystem _mapEditorSystem;
		
		private void Start()
		{
			_mapEditorModel = this.GetModel<IMapEditorModel>();
			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();
			
			Confirm.onClick.AddListener(() =>
			{
				_mapEditorSystem.SaveItemsToXml(_mapEditorModel.CurrentArchiveName.Value);
				this.gameObject.Hide();
			});
			
			Cancel.onClick.AddListener(() =>
			{
				this.gameObject.Hide();
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