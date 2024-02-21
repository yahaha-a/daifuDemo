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
	public partial class UISettlePanel : UIElement
	{
		private void Awake()
		{
			ConfirmButton.onClick.AddListener(() =>
			{
				this.Hide();
				UIKit.ClosePanel<UIGamePanel>();
				UIKit.OpenPanel<UIGamePassPanel>();
			});
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}