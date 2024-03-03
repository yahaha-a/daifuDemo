/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ReplaceConfirmPanel
	{
		[SerializeField] public UnityEngine.UI.Text Content;
		[SerializeField] public UnityEngine.UI.Button ConfirmButton;
		[SerializeField] public UnityEngine.UI.Button CancelButton;

		public void Clear()
		{
			Content = null;
			ConfirmButton = null;
			CancelButton = null;
		}

		public override string ComponentName
		{
			get { return "ReplaceConfirmPanel";}
		}
	}
}
