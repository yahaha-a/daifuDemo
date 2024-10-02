/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class NewMapPanel
	{
		[SerializeField] public UnityEngine.UI.InputField Name;
		[SerializeField] public UnityEngine.UI.Button Confirm;
		[SerializeField] public UnityEngine.UI.Button Cancel;

		public void Clear()
		{
			Name = null;
			Confirm = null;
			Cancel = null;
		}

		public override string ComponentName
		{
			get { return "NewMapPanel";}
		}
	}
}
