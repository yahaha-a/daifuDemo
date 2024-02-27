/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MenuDetail
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Text Price;
		[SerializeField] public UnityEngine.UI.Text Evaluate;
		[SerializeField] public UnityEngine.UI.Text Copies;
		[SerializeField] public UnityEngine.UI.Text Describe;
		[SerializeField] public RectTransform NeedFoodRoot;
		[SerializeField] public NeedFoodTemplate NeedFoodTemplate;

		public void Clear()
		{
			Name = null;
			Icon = null;
			Rank = null;
			Price = null;
			Evaluate = null;
			Copies = null;
			Describe = null;
			NeedFoodRoot = null;
			NeedFoodTemplate = null;
		}

		public override string ComponentName
		{
			get { return "MenuDetail";}
		}
	}
}
