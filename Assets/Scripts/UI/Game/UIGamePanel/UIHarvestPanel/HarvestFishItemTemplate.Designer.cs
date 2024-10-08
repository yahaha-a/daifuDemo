/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class HarvestFishItemTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Amount;

		public void Clear()
		{
			Icon = null;
			Name = null;
			Amount = null;
		}

		public override string ComponentName
		{
			get { return "HarvestFishItemTemplate";}
		}
	}
}
