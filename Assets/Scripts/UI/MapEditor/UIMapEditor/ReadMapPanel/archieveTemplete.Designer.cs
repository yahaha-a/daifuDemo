/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class archieveTemplete
	{
		[SerializeField] public UnityEngine.UI.Text archieveName;

		public void Clear()
		{
			archieveName = null;
		}

		public override string ComponentName
		{
			get { return "archieveTemplete";}
		}
	}
}
