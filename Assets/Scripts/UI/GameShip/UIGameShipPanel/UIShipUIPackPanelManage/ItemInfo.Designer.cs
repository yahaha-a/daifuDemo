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
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Description;
		[SerializeField] public UnityEngine.UI.Text Amount;

		public void Clear()
		{
			Name = null;
			Description = null;
			Amount = null;
		}

		public override string ComponentName
		{
			get { return "ItemInfo";}
		}
	}
}
