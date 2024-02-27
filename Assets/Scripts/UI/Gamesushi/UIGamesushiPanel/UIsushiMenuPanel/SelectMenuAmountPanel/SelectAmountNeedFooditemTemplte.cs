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
	public partial class SelectAmountNeedFooditemTemplte : UIElement
	{
		public int needAmount;
		public string backPackKey;
		
		private void Awake()
		{
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}