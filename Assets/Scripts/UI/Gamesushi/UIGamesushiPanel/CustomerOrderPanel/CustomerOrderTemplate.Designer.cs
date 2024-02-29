/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CustomerOrderTemplate
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			Name = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "CustomerOrderTemplate";}
		}
	}
}
