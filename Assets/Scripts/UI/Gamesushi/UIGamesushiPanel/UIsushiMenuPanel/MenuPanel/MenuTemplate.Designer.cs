/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MenuTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Text Amount;

		public void Clear()
		{
			Icon = null;
			Rank = null;
			Amount = null;
		}

		public override string ComponentName
		{
			get { return "MenuTemplate";}
		}
	}
}
