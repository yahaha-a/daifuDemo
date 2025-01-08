/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MeleeWeaponButton
	{
		[SerializeField] public UnityEngine.UI.Text Name;

		public void Clear()
		{
			Name = null;
		}

		public override string ComponentName
		{
			get { return "MeleeWeaponButton";}
		}
	}
}
