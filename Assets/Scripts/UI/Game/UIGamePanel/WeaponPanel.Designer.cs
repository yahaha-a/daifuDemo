/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class WeaponPanel
	{
		[SerializeField] public UnityEngine.UI.Button FishFork;
		[SerializeField] public UnityEngine.UI.Button Knife;
		[SerializeField] public UnityEngine.UI.Button PrimaryWeapon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Ammunition;
		[SerializeField] public UnityEngine.UI.Button SecondaryWeapons;

		public void Clear()
		{
			FishFork = null;
			Knife = null;
			PrimaryWeapon = null;
			Name = null;
			Ammunition = null;
			SecondaryWeapons = null;
		}

		public override string ComponentName
		{
			get { return "WeaponPanel";}
		}
	}
}
