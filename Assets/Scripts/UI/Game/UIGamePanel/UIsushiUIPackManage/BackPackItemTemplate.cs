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
	public partial class BackPackItemTemplate : UIElement
	{
		public string description;

		private void OnEnable()
		{
			this.GetComponent<Button>().onClick.AddListener(() =>
			{
				Events.UIsushiBackPackPanelItemInfoUpdate?.Trigger(this);
			});
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}