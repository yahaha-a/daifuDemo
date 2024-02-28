/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIsushiIngredientPanelManage
	{
		[SerializeField] public RectTransform UIsushiIngredientPackPanel;
		[SerializeField] public UnityEngine.UI.Button FishButton;
		[SerializeField] public UnityEngine.UI.Button IngredientButton;
		[SerializeField] public UnityEngine.UI.Button SeasoningButton;
		[SerializeField] public RectTransform BackPackFishItemListRoot;
		[SerializeField] public sushiBackPackItemTemplate sushiBackPackItemTemplate;
		[SerializeField] public RectTransform UIsushiInredientDetailedMessage;
		[SerializeField] public UnityEngine.UI.Text ItemName;
		[SerializeField] public UnityEngine.UI.Image ItemIcon;
		[SerializeField] public UnityEngine.UI.Text ItemDescription;
		[SerializeField] public UnityEngine.UI.Text ItemCount;
		[SerializeField] public RectTransform OptionMenuRoot;
		[SerializeField] public OptionMenuItemTemplate OptionMenuItemTemplate;

		public void Clear()
		{
			UIsushiIngredientPackPanel = null;
			FishButton = null;
			IngredientButton = null;
			SeasoningButton = null;
			BackPackFishItemListRoot = null;
			sushiBackPackItemTemplate = null;
			UIsushiInredientDetailedMessage = null;
			ItemName = null;
			ItemIcon = null;
			ItemDescription = null;
			ItemCount = null;
			OptionMenuRoot = null;
			OptionMenuItemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "UIsushiIngredientPanelManage";}
		}
	}
}
