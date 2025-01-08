/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class WeaponItemTemplete
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text EquipState;

		public void Clear()
		{
			Name = null;
			EquipState = null;
		}

		public override string ComponentName
		{
			get { return "WeaponItemTemplete";}
		}
	}
}
