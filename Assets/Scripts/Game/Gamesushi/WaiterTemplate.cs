using System;
using System.Linq;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum WaiterState
	{
		Wait,
		TakeFood,
		DeliveredFood
	}
	
	public partial class WaiterTemplate : ViewController, IController
	{
		public IstaffItemInfo StaffItemInfo { get; set; }
		
		public Vector2 StartPosition { get; set; }

		private ITableItemInfo _targetTable;

		private Vector2 _kitchenPosition = new Vector2(4, 1);

		private WaiterState _state = WaiterState.Wait;

		private string _currentTakeMealKey;

		private IStaffSystem _staffSystem;

		private IMenuSystem _menuSystem;

		private ICustomerSystem _customerSystem;

		private void Start()
		{
			_staffSystem = this.GetSystem<IStaffSystem>();

			_menuSystem = this.GetSystem<IMenuSystem>();

			_customerSystem = this.GetSystem<ICustomerSystem>();
			
			transform.position = StartPosition;
		}

		private void Update()
		{
			switch (_state)
			{
				case WaiterState.Wait:
					Wait();
					break;
				case WaiterState.TakeFood:
					TakeFood();
					break;
				case WaiterState.DeliveredFood:
					DeliveredFood();
					break;
			}
		}

		private void Wait()
		{
			if (_menuSystem.FinishedDishes.Count > 0)
			{
				_state = WaiterState.TakeFood;
			}
			else
			{
				if (Walk(StartPosition))
				{
					
				}
			}
		}

		private void TakeFood()
		{
			if (_menuSystem.FinishedDishes.Count > 0)
			{
				if (Walk(_kitchenPosition))
				{
					_currentTakeMealKey = _menuSystem.TakeAFinishedDish();

					if (_currentTakeMealKey != null)
					{
						Events.TakeFirstFinishedDish?.Trigger();
					}
					
					_targetTable = _customerSystem.TableItems.FirstOrDefault(item =>
						item.CustomerItemInfo?.CurrentOrderKey.Value == _currentTakeMealKey);
					
					_state = WaiterState.DeliveredFood;
				}
			}
			else
			{
				_state = WaiterState.Wait;
			}
		}

		private void DeliveredFood()
		{
			var targetPosition = _targetTable.CurrentPosition;
			if (Walk(targetPosition))
			{
				if (_targetTable.CustomerItemInfo != null &&
				    _targetTable.CustomerItemInfo.CurrentOrderKey.Value == _currentTakeMealKey)
				{
					_targetTable.CustomerItemInfo.WithIfReceiveOrderDish(true);
					_currentTakeMealKey = null;
					_state = WaiterState.Wait;
				}
				else
				{
					_state = WaiterState.Wait;
				}
			}
		}

		private bool Walk(Vector2 targetPosition)
		{
			if (Vector2.Distance(transform.position, targetPosition) > 0.1f)
			{
				Vector2 currentPosition = transform.position;
				Vector2 direction = (targetPosition - currentPosition).normalized;
				Vector2 speed = _staffSystem.StaffItemInfos[StaffItemInfo.Key].RankWithWalkSpeed
					.FirstOrDefault(item => item.Item1 == StaffItemInfo.Rank).Item2 * direction;
				transform.position = currentPosition + speed * Time.deltaTime;
			}
			else
			{
				return true;
			}

			return false;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
