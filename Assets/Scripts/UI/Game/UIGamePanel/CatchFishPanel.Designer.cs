/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CatchFishPanel
	{
		[SerializeField] public UnityEngine.UI.Slider ProgressBar;

		public void Clear()
		{
			ProgressBar = null;
		}

		public override string ComponentName
		{
			get { return "CatchFishPanel";}
		}
	}
}
