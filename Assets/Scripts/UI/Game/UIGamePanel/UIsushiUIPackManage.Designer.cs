/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIsushiUIPackManage
	{
		[SerializeField] public UnityEngine.UI.Button FishButton;
		[SerializeField] public UnityEngine.UI.Button IngredientButton;
		[SerializeField] public UnityEngine.UI.Button SeasoningButton;
		[SerializeField] public UnityEngine.UI.Button ToolButton;
		[SerializeField] public RectTransform BackPackItemListRoot;
		[SerializeField] public BackPackItemTemplate BackPackItemTemplate;
		[SerializeField] public ItemInfo ItemInfo;

		public void Clear()
		{
			FishButton = null;
			IngredientButton = null;
			SeasoningButton = null;
			ToolButton = null;
			BackPackItemListRoot = null;
			BackPackItemTemplate = null;
			ItemInfo = null;
		}

		public override string ComponentName
		{
			get { return "UIsushiUIPackManage";}
		}
	}
}
