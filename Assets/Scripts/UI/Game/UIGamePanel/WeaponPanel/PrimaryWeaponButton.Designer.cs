/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class PrimaryWeaponButton
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Type;
		[SerializeField] public UnityEngine.UI.Text Ammunition;

		public void Clear()
		{
			Name = null;
			Type = null;
			Ammunition = null;
		}

		public override string ComponentName
		{
			get { return "PrimaryWeaponButton";}
		}
	}
}
