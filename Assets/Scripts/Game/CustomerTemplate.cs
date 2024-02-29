using System;
using System.Linq;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CustomerTemplate : ViewController, IController
	{
		public ICustomerItemInfo CustomerItemInfo { get; set; }

		public Vector2 StartPosition { get; set; } = new Vector2(0, 0);
		
		public Vector2 TargetPosition { get; set; } = new Vector2(0, 0);

		private void Update()
		{
			if (CustomerItemInfo.State == CustomerItemState.Walk)
			{
				Walk();
			}
		}

		public void Walk()
		{
			Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
    
			Vector2 direction = (TargetPosition - currentPosition).normalized;

			Vector2 newPosition = currentPosition + direction * CustomerItemInfo.WalkSpeed * Time.deltaTime;

			transform.position = new Vector2(newPosition.x, newPosition.y);

			if (Vector2.Distance(newPosition, TargetPosition) < 0.1f)
			{
				CustomerItemInfo.WithState(CustomerItemState.Eat);
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
