﻿/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectMapPanel
	{
		[SerializeField] public RectTransform mapRoot;
		[SerializeField] public mapTemplete mapTemplete;
		[SerializeField] public UnityEngine.UI.Button Confirm;
		[SerializeField] public UnityEngine.UI.Button Cancel;

		public void Clear()
		{
			mapRoot = null;
			mapTemplete = null;
			Confirm = null;
			Cancel = null;
		}

		public override string ComponentName
		{
			get { return "SelectMapPanel";}
		}
	}
}
