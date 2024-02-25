/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class sushiBackPackItemTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Count;

		public void Clear()
		{
			Icon = null;
			Name = null;
			Count = null;
		}

		public override string ComponentName
		{
			get { return "sushiBackPackItemTemplate";}
		}
	}
}
