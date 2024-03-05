/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIAllTimePanel : UIElement, IController
	{
		private ICollectionModel _collectionModel;
		
		private void Start()
		{
			_collectionModel = this.GetModel<ICollectionModel>();

			_collectionModel.Gold.RegisterWithInitValue(value =>
			{
				Gold.text = "$: " + ((int)value).ToString();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
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