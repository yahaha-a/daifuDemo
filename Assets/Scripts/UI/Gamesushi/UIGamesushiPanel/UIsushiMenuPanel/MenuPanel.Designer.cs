/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MenuPanel
	{
		[SerializeField] public RectTransform MenuListRoot;
		[SerializeField] public MenuTemplate MenuTemplate;

		public void Clear()
		{
			MenuListRoot = null;
			MenuTemplate = null;
		}

		public override string ComponentName
		{
			get { return "MenuPanel";}
		}
	}
}
