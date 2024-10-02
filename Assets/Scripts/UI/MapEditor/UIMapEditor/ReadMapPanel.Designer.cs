/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class ReadMapPanel
	{
		[SerializeField] public RectTransform archieveRoot;
		[SerializeField] public archieveTemplete archieveTemplete;
		[SerializeField] public UnityEngine.UI.Button Cancel;
		[SerializeField] public UnityEngine.UI.Button Confirm;

		public void Clear()
		{
			archieveRoot = null;
			archieveTemplete = null;
			Cancel = null;
			Confirm = null;
		}

		public override string ComponentName
		{
			get { return "ReadMapPanel";}
		}
	}
}
