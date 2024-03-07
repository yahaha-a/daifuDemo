/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MenuItemTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;

		public void Clear()
		{
			Icon = null;
			Name = null;
		}

		public override string ComponentName
		{
			get { return "MenuItemTemplate";}
		}
	}
}
