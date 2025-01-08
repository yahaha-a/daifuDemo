/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectBulletTypePanel
	{
		[SerializeField] public UnityEngine.UI.Dropdown SelectBulletList;
		[SerializeField] public UnityEngine.UI.Text CountName;
		[SerializeField] public UnityEngine.UI.InputField Count;
		[SerializeField] public UnityEngine.UI.Text AllPrice;
		[SerializeField] public UnityEngine.UI.Button Confirm;

		public void Clear()
		{
			SelectBulletList = null;
			CountName = null;
			Count = null;
			AllPrice = null;
			Confirm = null;
		}

		public override string ComponentName
		{
			get { return "SelectBulletTypePanel";}
		}
	}
}
