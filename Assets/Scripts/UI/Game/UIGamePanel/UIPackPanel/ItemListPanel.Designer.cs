/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ItemListPanel
	{
		[SerializeField] public RectTransform ItemListRoot;
		[SerializeField] public ItemTemplate ItemTemplate;

		public void Clear()
		{
			ItemListRoot = null;
			ItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "ItemListPanel";}
		}
	}
}
