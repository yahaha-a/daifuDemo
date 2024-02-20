/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ItemInfo : UIElement
	{
		private void Awake()
		{
			Events.UIsushiBackPackPanelItemInfoUpdate.Register(item =>
			{
				ItemName.text = item.Name.text;
				ItemDescription.text = item.description;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}