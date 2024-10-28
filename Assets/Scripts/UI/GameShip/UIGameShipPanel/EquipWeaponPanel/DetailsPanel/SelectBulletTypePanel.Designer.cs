/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectBulletTypePanel
	{
		[SerializeField] public UnityEngine.UI.Dropdown SelectBulletList;
		[SerializeField] public UnityEngine.UI.InputField Count;
		[SerializeField] public UnityEngine.UI.Button Confirm;

		public void Clear()
		{
			SelectBulletList = null;
			Count = null;
			Confirm = null;
		}

		public override string ComponentName
		{
			get { return "SelectBulletTypePanel";}
		}
	}
}
