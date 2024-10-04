/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class InputCreateNumberPanel
	{
		[SerializeField] public UnityEngine.UI.InputField InputCreateNumber;
		[SerializeField] public UnityEngine.UI.Button Confirm;
		[SerializeField] public UnityEngine.UI.Button Cancel;

		public void Clear()
		{
			InputCreateNumber = null;
			Confirm = null;
			Cancel = null;
		}

		public override string ComponentName
		{
			get { return "InputCreateNumberPanel";}
		}
	}
}
