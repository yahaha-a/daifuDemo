/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UpgradeMenuPanel
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public CurrentData CurrentData;
		[SerializeField] public UpgradeData UpgradeData;
		[SerializeField] public RectTransform NeeedFoodListRoot;
		[SerializeField] public UpgradeNeedFooditemTemplte UpgradeNeedFooditemTemplte;
		[SerializeField] public UnityEngine.UI.Button ConfirmButton;
		[SerializeField] public UnityEngine.UI.Button CloseButton;

		public void Clear()
		{
			Icon = null;
			CurrentData = null;
			UpgradeData = null;
			NeeedFoodListRoot = null;
			UpgradeNeedFooditemTemplte = null;
			ConfirmButton = null;
			CloseButton = null;
		}

		public override string ComponentName
		{
			get { return "UpgradeMenuPanel";}
		}
	}
}
