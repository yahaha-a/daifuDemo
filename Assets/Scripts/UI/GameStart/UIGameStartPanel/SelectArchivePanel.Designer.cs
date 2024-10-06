/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectArchivePanel
	{
		[SerializeField] public RectTransform archieveRoot;
		[SerializeField] public archiveTemplete archiveTemplete;
		[SerializeField] public UnityEngine.UI.Button Confirm;
		[SerializeField] public UnityEngine.UI.Button Cancel;

		public void Clear()
		{
			archieveRoot = null;
			archiveTemplete = null;
			Confirm = null;
			Cancel = null;
		}

		public override string ComponentName
		{
			get { return "SelectArchivePanel";}
		}
	}
}
