/****************************************************************************
 * 2025.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class TaskTemplete
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text State;

		public void Clear()
		{
			Name = null;
			State = null;
		}

		public override string ComponentName
		{
			get { return "TaskTemplete";}
		}
	}
}
