/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class WeaponPanel
	{
		[SerializeField] public FishForkButton FishForkButton;
		[SerializeField] public MeleeWeaponButton MeleeWeaponButton;
		[SerializeField] public PrimaryWeaponButton PrimaryWeaponButton;
		[SerializeField] public SecondaryWeaponButton SecondaryWeaponButton;

		public void Clear()
		{
			FishForkButton = null;
			MeleeWeaponButton = null;
			PrimaryWeaponButton = null;
			SecondaryWeaponButton = null;
		}

		public override string ComponentName
		{
			get { return "WeaponPanel";}
		}
	}
}
