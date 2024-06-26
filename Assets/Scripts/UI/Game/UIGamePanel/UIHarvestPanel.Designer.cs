/****************************************************************************
 * 2024.6 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIHarvestPanel
	{
		[SerializeField] public RectTransform HarvestFishRoot;
		[SerializeField] public HarvestFishItemTemplate HarvestFishItemTemplate;
		[SerializeField] public RectTransform HarvestFoodRoot;
		[SerializeField] public HarvestFoodItemTemplate HarvestFoodItemTemplate;
		[SerializeField] public UnityEngine.UI.Button CloseButton;

		public void Clear()
		{
			HarvestFishRoot = null;
			HarvestFishItemTemplate = null;
			HarvestFoodRoot = null;
			HarvestFoodItemTemplate = null;
			CloseButton = null;
		}

		public override string ComponentName
		{
			get { return "UIHarvestPanel";}
		}
	}
}
