/****************************************************************************
 * 2024.9 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using daifuDemo;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class SelectOptionsPanel : UIElement, IController
	{
		private List<GameObject> _selectOptions = new List<GameObject>();

		private IMapEditorSystem _mapEditorSystem;

		private IMapEditorModel _mapEditorModel;

		private void Start()
		{
			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();

			_mapEditorModel = this.GetModel<IMapEditorModel>();
			
			CancelButton.onClick.AddListener(() =>
			{
				_mapEditorModel.CurrentMapEditorName.Value = MapEditorName.Null;
			});
			
			foreach (var (mapEditorName, mapEditorInfo) in _mapEditorSystem._mapEditorInfos)
			{
				if (mapEditorInfo.OptionType == OptionType.Null)
				{
					continue;
				}
				OptionTemplete.InstantiateWithParent(OptionRoot).Self(self =>
				{
					self.MapEditorInfo = mapEditorInfo;

					self.Show();
					_selectOptions.Add(self.gameObject);
				});
			}
			
			this.GetUtility<IUtils>().AdjustContentHeight(OptionRoot);
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