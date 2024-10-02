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
	public partial class archieveTemplete : UIElement, IController
	{
		public string Name;

		private void Start()
		{
			archieveName.text = Name;

			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				this.GetModel<IMapEditorModel>().CurrentSelectArchiveName.Value = Name;
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