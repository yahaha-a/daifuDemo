using System;
using System.Linq;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace daifuDemo
{
	public partial class CustomerTemplate : ViewController, IController
	{
		public ICustomerItemInfo CustomerItemInfo { get; set; }

		public Vector2 StartPosition { get; set; } = new Vector2();
		
		public Vector2 TargetPosition { get; set; } = new Vector2();

		private IMenuSystem _menuSystem;

		private void Start()
		{
			_menuSystem = this.GetSystem<IMenuSystem>();

			CustomerItemInfo.CurrentOrderKey.Register(menuKey =>
			{
				_menuSystem.AddPreparationDishes(menuKey);
				
				Events.CreateCustomerOrderMenuIcon?.Trigger(
					new Vector2(Camera.main.WorldToScreenPoint(OrderIconPosition.transform.position).x,
						Camera.main.WorldToScreenPoint(OrderIconPosition.transform.position).y), menuKey);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			SwitchStateWithAction();
		}

		public void SwitchStateWithAction()
		{
			switch (CustomerItemInfo.State)
			{
				case CustomerItemState.Walk:
					Walk();
					break;
				case CustomerItemState.Order:
					Order();
					break;
				case CustomerItemState.Wait:
					Wait();
					break;
				case CustomerItemState.Drink:
					Drink();
					break;
				case CustomerItemState.Eat:
					Eat();
					break;
				case CustomerItemState.Leave:
					Leave();
					break;
				case CustomerItemState.Dead:
					Dead();
					break;
			}
		}
		
		private void Walk()
		{
			Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
    
			Vector2 direction = (TargetPosition - currentPosition).normalized;

			Vector2 newPosition = currentPosition + direction * CustomerItemInfo.WalkSpeed * Time.deltaTime;

			transform.position = new Vector2(newPosition.x, newPosition.y);

			if (Vector2.Distance(newPosition, TargetPosition) < 0.1f)
			{
				CustomerItemInfo.WithState(CustomerItemState.Order);
			}
		}

		private void Order()
		{
			CustomerItemInfo.OrderNeedTime -= Time.deltaTime;
			if (CustomerItemInfo.OrderNeedTime <= 0)
			{
				if (CustomerItemInfo.IfDrink)
				{
					
				}
				else
				{
					CustomerItemInfo.WithCurrentOrderKey(_menuSystem.GetARandomDish());
				}

				CustomerItemInfo.WithState(CustomerItemState.Wait);
			}
		}

		private void Wait()
		{
			CustomerItemInfo.WaitTime -= Time.deltaTime;

			if (CustomerItemInfo.WaitTime <= 0)
			{
				CustomerItemInfo.WithCurrentOrderKey(null);
				CustomerItemInfo.WithState(CustomerItemState.Leave);
			}

			if (CustomerItemInfo.IfReceiveOrderDish)
			{
				CustomerItemInfo.WithState(CustomerItemState.Eat);
			}
		}

		private void Drink()
		{
			CustomerItemInfo.WithCurrentOrderKey(null);
			CustomerItemInfo.WithTip(CustomerItemInfo.Tip * CustomerItemInfo.TipMultiple);
			CustomerItemInfo.WithIfDrink(false);
			CustomerItemInfo.WithState(CustomerItemState.Order);
		}

		private void Eat()
		{
			CustomerItemInfo.WithCurrentOrderKey(null);
			CustomerItemInfo.EatTime -= Time.deltaTime;
			
			if (CustomerItemInfo.EatTime <= 0)
			{
				CustomerItemInfo.WithState(CustomerItemState.Leave);
			}
		}

		private void Leave()
		{
			var currentPosition = new Vector2(transform.position.x, transform.position.y);
			TargetPosition = StartPosition;
			Vector2 direction = (TargetPosition - currentPosition).normalized;
			Vector2 newPosition = currentPosition + direction * CustomerItemInfo.WalkSpeed * Time.deltaTime;
			transform.position = new Vector2(newPosition.x, newPosition.y);
			if (Vector2.Distance(newPosition, TargetPosition) < 0.1f)
			{
				CustomerItemInfo.WithState(CustomerItemState.Dead);
			}
		}

		private void Dead()
		{
			this.gameObject.DestroySelf();
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
