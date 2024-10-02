/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class OptionTemplete
	{
		[SerializeField] public UnityEngine.UI.Text Name;

		public void Clear()
		{
			Name = null;
		}

		public override string ComponentName
		{
			get { return "OptionTemplete";}
		}
	}
}
