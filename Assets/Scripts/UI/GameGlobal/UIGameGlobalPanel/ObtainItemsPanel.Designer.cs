/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ObtainItemsPanel
	{
		[SerializeField] public RectTransform ObtainItemsTextPanel;
		[SerializeField] public ObtainItemsTextTemplete ObtainItemsTextTemplete;
		[SerializeField] public RectTransform ObtainItemsIconPanel;
		[SerializeField] public ObtainItemsIconTemplete ObtainItemsIconTemplete;

		public void Clear()
		{
			ObtainItemsTextPanel = null;
			ObtainItemsTextTemplete = null;
			ObtainItemsIconPanel = null;
			ObtainItemsIconTemplete = null;
		}

		public override string ComponentName
		{
			get { return "ObtainItemsPanel";}
		}
	}
}
