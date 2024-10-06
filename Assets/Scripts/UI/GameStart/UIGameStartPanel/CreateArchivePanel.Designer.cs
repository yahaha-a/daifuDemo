/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CreateArchivePanel
	{
		[SerializeField] public UnityEngine.UI.InputField ArchiveName;
		[SerializeField] public UnityEngine.UI.Button Confirm;
		[SerializeField] public UnityEngine.UI.Button Cancel;

		public void Clear()
		{
			ArchiveName = null;
			Confirm = null;
			Cancel = null;
		}

		public override string ComponentName
		{
			get { return "CreateArchivePanel";}
		}
	}
}
