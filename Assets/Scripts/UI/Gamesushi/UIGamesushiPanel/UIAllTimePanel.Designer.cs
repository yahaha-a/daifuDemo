/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIAllTimePanel
	{
		[SerializeField] public UnityEngine.UI.Text Gold;

		public void Clear()
		{
			Gold = null;
		}

		public override string ComponentName
		{
			get { return "UIAllTimePanel";}
		}
	}
}
