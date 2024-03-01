/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UIsushiMenuPanel
	{
		[SerializeField] public TodayMenuPanel TodayMenuPanel;
		[SerializeField] public MenuPanel MenuPanel;
		[SerializeField] public MenuDetail MenuDetail;
		[SerializeField] public SelectMenuAmountPanel SelectMenuAmountPanel;
		[SerializeField] public UpgradeMenuPanel UpgradeMenuPanel;

		public void Clear()
		{
			TodayMenuPanel = null;
			MenuPanel = null;
			MenuDetail = null;
			SelectMenuAmountPanel = null;
			UpgradeMenuPanel = null;
		}

		public override string ComponentName
		{
			get { return "UIsushiMenuPanel";}
		}
	}
}
