/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class GoShipPanel
	{
		[SerializeField] public UnityEngine.UI.Button ConfirmButton;
		[SerializeField] public UnityEngine.UI.Button CancelButton;

		public void Clear()
		{
			ConfirmButton = null;
			CancelButton = null;
		}

		public override string ComponentName
		{
			get { return "GoShipPanel";}
		}
	}
}
