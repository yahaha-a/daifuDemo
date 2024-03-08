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
	public partial class TaskTemplete : UIElement
	{
		public ITaskInfo TaskInfo { get; set; }
		
		private void Awake()
		{
			Name.text = TaskInfo.Name;

			if (TaskInfo.State.Value == TaskState.Executing)
			{
				State.text = "进行中";
			}
			else if (TaskInfo.State.Value == TaskState.Finished)
			{
				State.text = "已完成";
			}
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}