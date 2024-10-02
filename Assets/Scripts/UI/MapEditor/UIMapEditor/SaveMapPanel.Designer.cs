/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class SaveMapPanel
	{
		[SerializeField] public UnityEngine.UI.Button Cancel;
		[SerializeField] public UnityEngine.UI.Button Confirm;

		public void Clear()
		{
			Cancel = null;
			Confirm = null;
		}

		public override string ComponentName
		{
			get { return "SaveMapPanel";}
		}
	}
}
