/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class GoToSeaPanel
	{
		[SerializeField] public UnityEngine.UI.Button AgreeButton;
		[SerializeField] public UnityEngine.UI.Button CancelButton;

		public void Clear()
		{
			AgreeButton = null;
			CancelButton = null;
		}

		public override string ComponentName
		{
			get { return "GoToSeaPanel";}
		}
	}
}
