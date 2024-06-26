/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UISettlePanel
	{
		[SerializeField] public SettleItemManage SettleItemRoot;
		[SerializeField] public UnityEngine.UI.Button ConfirmButton;

		public void Clear()
		{
			SettleItemRoot = null;
			ConfirmButton = null;
		}

		public override string ComponentName
		{
			get { return "UISettlePanel";}
		}
	}
}
