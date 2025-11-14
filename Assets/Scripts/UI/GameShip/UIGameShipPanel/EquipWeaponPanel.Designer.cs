/****************************************************************************
 * 2025.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class EquipWeaponPanel
	{
		[SerializeField] public UnityEngine.UI.Button FishFork;
		[SerializeField] public UnityEngine.UI.Text FishForkName;
		[SerializeField] public UnityEngine.UI.Button Knife;
		[SerializeField] public UnityEngine.UI.Text MeleeWeaponName;
		[SerializeField] public UnityEngine.UI.Button PrimaryWeapon;
		[SerializeField] public UnityEngine.UI.Text PrimaryWeaponName;
		[SerializeField] public UnityEngine.UI.Button SecondaryWeapons;
		[SerializeField] public UnityEngine.UI.Text SecondaryWeaponName;
		[SerializeField] public SelectWeaponPanel SelectWeaponPanel;
		[SerializeField] public DetailsPanel DetailsPanel;
		[SerializeField] public UnityEngine.UI.Button Confirm;

		public void Clear()
		{
			FishFork = null;
			FishForkName = null;
			Knife = null;
			MeleeWeaponName = null;
			PrimaryWeapon = null;
			PrimaryWeaponName = null;
			SecondaryWeapons = null;
			SecondaryWeaponName = null;
			SelectWeaponPanel = null;
			DetailsPanel = null;
			Confirm = null;
		}

		public override string ComponentName
		{
			get { return "EquipWeaponPanel";}
		}
	}
}
