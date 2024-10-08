/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SettleItemTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Number;

		public void Clear()
		{
			Icon = null;
			Name = null;
			Number = null;
		}

		public override string ComponentName
		{
			get { return "SettleItemTemplate";}
		}
	}
}
