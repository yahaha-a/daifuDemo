/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class WeaponUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Text Details;

		public void Clear()
		{
			Details = null;
		}

		public override string ComponentName
		{
			get { return "WeaponUpgradePanel";}
		}
	}
}
