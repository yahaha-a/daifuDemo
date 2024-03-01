/****************************************************************************
 * 2024.3 WXH
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
		[SerializeField] public UnityEngine.UI.Button CloseButton;

		public void Clear()
		{
			MenuListRoot = null;
			MenuTemplate = null;
			CloseButton = null;
		}

		public override string ComponentName
		{
			get { return "MenuPanel";}
		}
	}
}
