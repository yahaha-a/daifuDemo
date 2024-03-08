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
	public partial class UITaskDisplayPanel : UIElement, IController
	{
		private ITaskModel _taskModel;

		private List<ProcessTemplete> _processItems = new List<ProcessTemplete>();
		
		private void Awake()
		{
			_taskModel = this.GetModel<ITaskModel>();

			_taskModel.CurrentTask.RegisterWithInitValue(task =>
			{
				if (task == null)
				{
					NullTask.Show();
					CurrentTask.Hide();
				}
				else
				{
					NullTask.Hide();
					CurrentTask.Show();
					
					Name.text = task.Name;
					Describe.text = task.Describe;

					foreach (var processItem in _processItems)
					{
						processItem.gameObject.DestroySelf();
					}

					_processItems.Clear();

					foreach (var taskItem in task.TaskItems)
					{
						ProcessTemplete.InstantiateWithParent(ProcessRoot).Self(self =>
						{
							self.TaskItem = taskItem;
							
							self.Show();
							_processItems.Add(self);
						});
					}
				}
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