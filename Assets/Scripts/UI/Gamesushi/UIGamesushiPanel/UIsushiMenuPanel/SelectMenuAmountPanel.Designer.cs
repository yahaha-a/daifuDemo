/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class SelectMenuAmountPanel
	{
		[SerializeField] public RectTransform NeeedFoodListRoot;
		[SerializeField] public SelectAmountNeedFooditemTemplte SelectAmountNeedFooditemTemplte;
		[SerializeField] public UnityEngine.UI.Text Amount;
		[SerializeField] public UnityEngine.UI.Button Reduce;
		[SerializeField] public UnityEngine.UI.Button Increase;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Button ConfirmButton;
		[SerializeField] public UnityEngine.UI.Button CloseButton;

		public void Clear()
		{
			NeeedFoodListRoot = null;
			SelectAmountNeedFooditemTemplte = null;
			Amount = null;
			Reduce = null;
			Increase = null;
			Icon = null;
			Name = null;
			ConfirmButton = null;
			CloseButton = null;
		}

		public override string ComponentName
		{
			get { return "SelectMenuAmountPanel";}
		}
	}
}
