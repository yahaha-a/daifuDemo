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
	public partial class NewMapPanel : UIElement, IController
	{
		private IMapEditorSystem _mapEditorSystem;
		private IMapEditorModel _mapEditorModel;
		
		private string _name;

		private void Start()
		{
			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();
			_mapEditorModel = this.GetModel<IMapEditorModel>();
			
			Confirm.onClick.AddListener(() =>
			{
				GetName();
				_mapEditorSystem.CreateEmptySave(_name);
				_mapEditorModel.CurrentArchiveName.Value = _name;
				
				this.gameObject.Hide();
			});
			
			Cancel.onClick.AddListener(() =>
			{
				this.gameObject.Hide();
			});
		}

		private void OnDisable()
		{
			_name = null;
		}

		private void GetName()
		{
			_name = Name.text;
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