/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class OptionMenuItemTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Text Money;

		public void Clear()
		{
			Icon = null;
			Name = null;
			Rank = null;
			Money = null;
		}

		public override string ComponentName
		{
			get { return "OptionMenuItemTemplate";}
		}
	}
}
