/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIPackPanel
	{
		[SerializeField] public RectTransform UIPackItemListRoot;
		[SerializeField] public UIPackItemTemplate UIPackItemTemplate;
		[SerializeField] public UIPackItemDetailPanel UIPackItemDetailPanel;

		public void Clear()
		{
			UIPackItemListRoot = null;
			UIPackItemTemplate = null;
			UIPackItemDetailPanel = null;
		}

		public override string ComponentName
		{
			get { return "UIPackPanel";}
		}
	}
}
