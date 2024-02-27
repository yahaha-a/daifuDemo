/****************************************************************************
 * 2024.2 WXH
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

		public void Clear()
		{
			TodayMenuPanel = null;
			MenuPanel = null;
			MenuDetail = null;
		}

		public override string ComponentName
		{
			get { return "UIsushiMenuPanel";}
		}
	}
}
