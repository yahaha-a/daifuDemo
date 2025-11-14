/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SecondaryWeaponButton
	{
		[SerializeField] public UnityEngine.UI.Text Level;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Type;
		[SerializeField] public UnityEngine.UI.Text Ammunition;

		public void Clear()
		{
			Level = null;
			Name = null;
			Type = null;
			Ammunition = null;
		}

		public override string ComponentName
		{
			get { return "SecondaryWeaponButton";}
		}
	}
}
