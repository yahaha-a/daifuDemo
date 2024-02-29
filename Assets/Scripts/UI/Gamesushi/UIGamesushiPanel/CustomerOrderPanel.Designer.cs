/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CustomerOrderPanel
	{
		[SerializeField] public RectTransform CustomerOrderRoot;
		[SerializeField] public CustomerOrderTemplate CustomerOrderTemplate;

		public void Clear()
		{
			CustomerOrderRoot = null;
			CustomerOrderTemplate = null;
		}

		public override string ComponentName
		{
			get { return "CustomerOrderPanel";}
		}
	}
}
