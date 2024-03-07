/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class NewMenuUnLockpanel
	{
		[SerializeField] public UnityEngine.UI.Text NotSelectShow;
		[SerializeField] public RectTransform SelectShow;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Gold;
		[SerializeField] public UnityEngine.UI.Text Score;
		[SerializeField] public UnityEngine.UI.Text Dishes;
		[SerializeField] public UnityEngine.UI.Text Description;
		[SerializeField] public UnityEngine.UI.Text NeedAndHaveGold;
		[SerializeField] public UnityEngine.UI.Button ConfirmButton;
		[SerializeField] public UnityEngine.UI.Button CloseButton;

		public void Clear()
		{
			NotSelectShow = null;
			SelectShow = null;
			Name = null;
			Icon = null;
			Gold = null;
			Score = null;
			Dishes = null;
			Description = null;
			NeedAndHaveGold = null;
			ConfirmButton = null;
			CloseButton = null;
		}

		public override string ComponentName
		{
			get { return "NewMenuUnLockpanel";}
		}
	}
}
