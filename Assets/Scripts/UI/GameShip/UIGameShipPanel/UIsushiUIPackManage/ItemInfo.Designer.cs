/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ItemInfo
	{
		[SerializeField] public UnityEngine.UI.Image ItemIcon;
		[SerializeField] public UnityEngine.UI.Text ItemDescription;
		[SerializeField] public UnityEngine.UI.Text ItemName;

		public void Clear()
		{
			ItemIcon = null;
			ItemDescription = null;
			ItemName = null;
		}

		public override string ComponentName
		{
			get { return "ItemInfo";}
		}
	}
}
