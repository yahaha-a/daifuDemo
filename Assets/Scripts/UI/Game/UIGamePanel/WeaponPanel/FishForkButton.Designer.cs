/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class FishForkButton
	{
		[SerializeField] public UnityEngine.UI.Text Level;
		[SerializeField] public UnityEngine.UI.Text Name;

		public void Clear()
		{
			Level = null;
			Name = null;
		}

		public override string ComponentName
		{
			get { return "FishForkButton";}
		}
	}
}
