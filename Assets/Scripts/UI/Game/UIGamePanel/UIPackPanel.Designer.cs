/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIPackPanel
	{
		[SerializeField] public ItemListPanel ItemListPanel;

		public void Clear()
		{
			ItemListPanel = null;
		}

		public override string ComponentName
		{
			get { return "UIPackPanel";}
		}
	}
}
