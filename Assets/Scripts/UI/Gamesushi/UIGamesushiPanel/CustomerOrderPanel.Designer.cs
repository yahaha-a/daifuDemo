/****************************************************************************
 * 2024.3 WXH
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
		[SerializeField] public RectTransform MakingAndFinishedDishesRoot;
		[SerializeField] public MakAndFinDishesTemplate MakAndFinDishesTemplate;

		public void Clear()
		{
			CustomerOrderRoot = null;
			CustomerOrderTemplate = null;
			MakingAndFinishedDishesRoot = null;
			MakAndFinDishesTemplate = null;
		}

		public override string ComponentName
		{
			get { return "CustomerOrderPanel";}
		}
	}
}
