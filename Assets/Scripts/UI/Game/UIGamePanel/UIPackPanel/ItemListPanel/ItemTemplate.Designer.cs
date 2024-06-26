/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ItemTemplate
	{
		[SerializeField] public UnityEngine.UI.Image ItemIcon;
		[SerializeField] public UnityEngine.UI.Text ItemName;
		[SerializeField] public UnityEngine.UI.Text ItemStar;
		[SerializeField] public UnityEngine.UI.Text ItemAmount;

		public void Clear()
		{
			ItemIcon = null;
			ItemName = null;
			ItemStar = null;
			ItemAmount = null;
		}

		public override string ComponentName
		{
			get { return "ItemTemplate";}
		}
	}
}
