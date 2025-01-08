/****************************************************************************
 * 2024.11 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIPackItemDetailPanel
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Detail;

		public void Clear()
		{
			Icon = null;
			Name = null;
			Detail = null;
		}

		public override string ComponentName
		{
			get { return "UIPackItemDetailPanel";}
		}
	}
}
