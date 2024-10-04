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
	public partial class InputCreateNumberPanel : UIElement, IController
	{
		private IMapEditorModel _mapEditorModel;

		private void Awake()
		{
			_mapEditorModel = this.GetModel<IMapEditorModel>();
		}

		private void Start()
		{
			Confirm.onClick.AddListener((() =>
			{
				int createNumber;
				if (int.TryParse(InputCreateNumber.text, out createNumber))
				{
					_mapEditorModel.CurrentCreateItemNumber.Value = createNumber;
				}
				else
				{
					_mapEditorModel.CurrentCreateItemNumber.Value = 1;
				}

				transform.gameObject.Hide();
			}));
			
			Cancel.onClick.AddListener(() =>
			{
				transform.gameObject.Hide();
			});
		}

		private void OnEnable()
		{
			_mapEditorModel.IfInputCreateNumberPanelShow.Value = true;
		}

		private void OnDisable()
		{
			_mapEditorModel.IfInputCreateNumberPanelShow.Value = false;
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