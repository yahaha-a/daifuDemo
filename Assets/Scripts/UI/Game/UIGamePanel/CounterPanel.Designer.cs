/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CounterPanel
	{
		[SerializeField] public UnityEngine.UI.Text title;
		[SerializeField] public UnityEngine.UI.Slider ProgressBar;

		public void Clear()
		{
			title = null;
			ProgressBar = null;
		}

		public override string ComponentName
		{
			get { return "CounterPanel";}
		}
	}
}
