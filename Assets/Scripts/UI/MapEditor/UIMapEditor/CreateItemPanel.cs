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
	public partial class CreateItemPanel : UIElement, IController
	{
		private IMapEditorSystem _mapEditorSystem;

		private List<GameObject> _createItemLists = new List<GameObject>();

		private void Start()
		{
			_mapEditorSystem = this.GetSystem<IMapEditorSystem>();

			MapEditorEvents.refreshCreatePanel.Register(() =>
			{
				foreach (var item in _createItemLists)
				{
					item.DestroySelf();
				}
				_createItemLists.Clear();
				
				foreach (var (key, createItemInfos) in _mapEditorSystem.FishCreateItems)
				{
					foreach (var createItemInfo in createItemInfos)
					{
						CreateItemTemplete.InstantiateWithParent(CreateItemRoot).Self(self =>
						{
							self.CreateItemInfo = createItemInfo;

							self.Show();
							_createItemLists.Add(self.gameObject);
						});
					}
				}

				foreach (var (key, createItemInfos) in _mapEditorSystem.TreasureChestsItems)
				{
					foreach (var createItemInfo in createItemInfos)
					{
						CreateItemTemplete.InstantiateWithParent(CreateItemRoot).Self(self =>
						{
							self.CreateItemInfo = createItemInfo;

							self.Show();
							_createItemLists.Add(self.gameObject);
						});
					}
				}

				foreach (var (key, createItemInfos) in _mapEditorSystem.DestructibleItems)
				{
					foreach (var createItemInfo in createItemInfos)
					{
						CreateItemTemplete.InstantiateWithParent(CreateItemRoot).Self(self =>
						{
							self.CreateItemInfo = createItemInfo;

							self.Show();
							_createItemLists.Add(self.gameObject);
						});
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			MapEditorEvents.ShowCreateItem.Register(currentCreateItem =>
			{
				CreateItemTemplete.InstantiateWithParent(CreateItemRoot).Self(self =>
				{
					self.CreateItemInfo = currentCreateItem;

					self.Show();
					_createItemLists.Add(self.gameObject);
				});
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
			foreach (var item in _createItemLists)
			{
				item.DestroySelf();
			}
			_createItemLists.Clear();
		}

		public IArchitecture GetArchitecture()
		{
			return MapEditorGlobal.Interface;
		}
	}
}