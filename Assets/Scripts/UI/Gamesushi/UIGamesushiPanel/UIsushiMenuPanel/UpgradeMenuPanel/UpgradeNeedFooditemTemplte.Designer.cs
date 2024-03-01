/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UpgradeNeedFooditemTemplte
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text NeedAndOwnAmount;

		public void Clear()
		{
			Icon = null;
			Name = null;
			NeedAndOwnAmount = null;
		}

		public override string ComponentName
		{
			get { return "UpgradeNeedFooditemTemplte";}
		}
	}
}
