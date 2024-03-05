/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class TodayMenuTemplate
	{
		[SerializeField] public RectTransform MenuMessage;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Amount;
		[SerializeField] public RectTransform Options;
		[SerializeField] public UnityEngine.UI.Button AddButton;
		[SerializeField] public UnityEngine.UI.Button ReduceButton;
		[SerializeField] public UnityEngine.UI.Button AutoButton;
		[SerializeField] public UnityEngine.UI.Button RemoveButton;
		[SerializeField] public UnityEngine.UI.Image AddMenuMessage;

		public void Clear()
		{
			MenuMessage = null;
			Icon = null;
			Rank = null;
			Name = null;
			Amount = null;
			Options = null;
			AddButton = null;
			ReduceButton = null;
			AutoButton = null;
			RemoveButton = null;
			AddMenuMessage = null;
		}

		public override string ComponentName
		{
			get { return "TodayMenuTemplate";}
		}
	}
}
