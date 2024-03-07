/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class NewMenuBagPanel
	{
		[SerializeField] public RectTransform MenuItemRoot;
		[SerializeField] public MenuItemTemplate MenuItemTemplate;

		public void Clear()
		{
			MenuItemRoot = null;
			MenuItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "NewMenuBagPanel";}
		}
	}
}
