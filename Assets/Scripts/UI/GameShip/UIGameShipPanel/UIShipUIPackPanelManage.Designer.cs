/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIShipUIPackPanelManage
	{
		[SerializeField] public RectTransform BackPackItemListRoot;
		[SerializeField] public BackPackItemTemplate BackPackItemTemplate;
		[SerializeField] public UnityEngine.UI.Button CloseButton;
		[SerializeField] public ItemInfo ItemInfo;

		public void Clear()
		{
			BackPackItemListRoot = null;
			BackPackItemTemplate = null;
			CloseButton = null;
			ItemInfo = null;
		}

		public override string ComponentName
		{
			get { return "UIShipUIPackPanelManage";}
		}
	}
}
