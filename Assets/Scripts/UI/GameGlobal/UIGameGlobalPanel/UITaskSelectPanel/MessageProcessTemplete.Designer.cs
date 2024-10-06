/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MessageProcessTemplete
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text OwnAndNeedAmount;

		public void Clear()
		{
			Name = null;
			OwnAndNeedAmount = null;
		}

		public override string ComponentName
		{
			get { return "MessageProcessTemplete";}
		}
	}
}
