/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UINewMenuUnLockPanel
	{
		[SerializeField] public NewMenuBagPanel NewMenuBagPanel;
		[SerializeField] public NewMenuUnLockpanel NewMenuUnLockpanel;

		public void Clear()
		{
			NewMenuBagPanel = null;
			NewMenuUnLockpanel = null;
		}

		public override string ComponentName
		{
			get { return "UINewMenuUnLockPanel";}
		}
	}
}
