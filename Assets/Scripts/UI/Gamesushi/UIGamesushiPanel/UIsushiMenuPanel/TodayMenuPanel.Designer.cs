/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class TodayMenuPanel
	{
		[SerializeField] public RectTransform TodayMeunListRoot;
		[SerializeField] public TodayMenuTemplate TodayMenuTemplate;

		public void Clear()
		{
			TodayMeunListRoot = null;
			TodayMenuTemplate = null;
		}

		public override string ComponentName
		{
			get { return "TodayMenuPanel";}
		}
	}
}
