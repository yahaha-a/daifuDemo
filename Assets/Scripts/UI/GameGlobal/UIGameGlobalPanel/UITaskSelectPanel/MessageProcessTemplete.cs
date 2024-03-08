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
	public partial class MessageProcessTemplete : UIElement
	{
		public TaskItem TaskItem { get; set; }
		
		private void Awake()
		{
			Name.text = TaskItem.Name;

			TaskItem.CurrentAmount.RegisterWithInitValue(value =>
			{
				OwnAndNeedAmount.text = value + " / " + TaskItem.TargetAmount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}