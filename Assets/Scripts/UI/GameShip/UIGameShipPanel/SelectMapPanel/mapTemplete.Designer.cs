/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class mapTemplete
	{
		[SerializeField] public UnityEngine.UI.Text mapName;

		public void Clear()
		{
			mapName = null;
		}

		public override string ComponentName
		{
			get { return "mapTemplete";}
		}
	}
}
