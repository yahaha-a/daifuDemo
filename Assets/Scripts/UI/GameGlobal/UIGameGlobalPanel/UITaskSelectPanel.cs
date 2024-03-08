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
	public partial class UITaskSelectPanel : UIElement, IController
	{
		private ITaskSystem _taskSystem;

		private ITaskModel _taskModel;

		private BindableProperty<ITaskInfo> _currentSelectTaskInfo = new BindableProperty<ITaskInfo>();

		private List<TaskTemplete> _taskItems = new List<TaskTemplete>();

		private List<MessageProcessTemplete> _precessItems = new List<MessageProcessTemplete>();
		
		private void Awake()
		{
			_taskSystem = this.GetSystem<ITaskSystem>();
			_taskModel = this.GetModel<ITaskModel>();
		}

		private void Start()
		{
			CloseButton.onClick.AddListener(() =>
			{
				this.gameObject.Hide();
			});
			
			TaskButton.onClick.AddListener(() =>
			{
				_taskSystem.TraceTask(_currentSelectTaskInfo.Value);
			});
			
			_currentSelectTaskInfo.RegisterWithInitValue(taskInfo =>
			{
				if (taskInfo == null)
				{
					NullTask.Show();
					TaskMessage.Hide();
				}
				else
				{
					NullTask.Hide();
					TaskMessage.Show();

					foreach (var messageProcessItem in _precessItems)
					{
						messageProcessItem.gameObject.DestroySelf();
					}

					_precessItems.Clear();

					Name.text = _currentSelectTaskInfo.Value.Name;
					Describe.text = _currentSelectTaskInfo.Value.Describe;

					foreach (var taskItem in _currentSelectTaskInfo.Value.TaskItems)
					{
						MessageProcessTemplete.InstantiateWithParent(ProcessRoot).Self(self =>
						{
							self.TaskItem = taskItem;
							
							self.Show();
							_precessItems.Add(self);
						});
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			_currentSelectTaskInfo.Value = _taskModel.CurrentTask.Value;

			foreach (var taskKey in _taskSystem.CurrentTask)
			{
				TaskTemplete.InstantiateWithParent(TaskListRoot).Self(self =>
				{
					self.TaskInfo = _taskSystem.TaskNodes[taskKey];
					
					self.Show();
					
					self.GetComponent<Button>().onClick.AddListener(() =>
					{
						_currentSelectTaskInfo.Value = self.TaskInfo;
					});
					
					_taskItems.Add(self);
				});
			}
		}

		private void OnDisable()
		{
			foreach (var taskTemplete in _taskItems)
			{
				taskTemplete.gameObject.DestroySelf();
			}
			
			_taskItems.Clear();
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