/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SettleItemManage
	{
		[SerializeField] public SettleItemTemplate SettleItemTemplate;

		public void Clear()
		{
			SettleItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "SettleItemManage";}
		}
	}
}
