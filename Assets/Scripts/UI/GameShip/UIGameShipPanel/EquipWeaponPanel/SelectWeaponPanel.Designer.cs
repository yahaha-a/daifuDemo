/****************************************************************************
 * 2025.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectWeaponPanel
	{
		[SerializeField] public RectTransform WeaponItemRoot;
		[SerializeField] public WeaponItemTemplete WeaponItemTemplete;

		public void Clear()
		{
			WeaponItemRoot = null;
			WeaponItemTemplete = null;
		}

		public override string ComponentName
		{
			get { return "SelectWeaponPanel";}
		}
	}
}
