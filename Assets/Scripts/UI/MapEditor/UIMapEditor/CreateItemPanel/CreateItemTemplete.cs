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
	public partial class CreateItemTemplete : UIElement, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();
		
		private IMapEditorSystem _mapEditorSystem;
		
		public ICreateItemInfo CreateItemInfo;

		private BindableProperty<bool> _ifSelected = new BindableProperty<bool>(false);

		private void Start()
		{
			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();
			
			this.GetComponent<RectTransform>().position = new Vector3(CreateItemInfo.X, CreateItemInfo.Y, 0);
			this.GetComponent<RectTransform>().sizeDelta = new Vector2(CreateItemInfo.Range, CreateItemInfo.Range);
			if (_mapEditorSystem._mapEditorInfos[CreateItemInfo.Key].OptionType == OptionType.Null)
			{
				
			}
			else if (_mapEditorSystem._mapEditorInfos[CreateItemInfo.Key].OptionType == OptionType.Single)
			{
				Icon.sprite = _resLoader.LoadSync<Sprite>("SingleIcon");
			}
			else if (_mapEditorSystem._mapEditorInfos[CreateItemInfo.Key].OptionType == OptionType.Range)
			{
				Icon.sprite = _resLoader.LoadSync<Sprite>("RangeIcon");
			}

			Name.text = _mapEditorSystem._mapEditorInfos[CreateItemInfo.Key].Name;

			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				_ifSelected.Value = !_ifSelected.Value;
			});

			_ifSelected.RegisterWithInitValue(value =>
			{
				if (value)
				{
					MapEditorEvents.DeleteCreateItem?.Trigger(CreateItemInfo.SerialNumber);
				}
				else
				{

				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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