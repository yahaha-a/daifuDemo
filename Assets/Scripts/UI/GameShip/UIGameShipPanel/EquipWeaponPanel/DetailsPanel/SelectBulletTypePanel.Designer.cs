/****************************************************************************
 * 2025.2 WXH
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

		public void Clear()
		{
			SelectBulletList = null;
			CountName = null;
		}

		public override string ComponentName
		{
			get { return "SelectBulletTypePanel";}
		}
	}
}
