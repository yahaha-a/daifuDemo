/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class CurrentData
	{
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Text Cost;
		[SerializeField] public UnityEngine.UI.Text Score;
		[SerializeField] public UnityEngine.UI.Text Copies;

		public void Clear()
		{
			Rank = null;
			Cost = null;
			Score = null;
			Copies = null;
		}

		public override string ComponentName
		{
			get { return "CurrentData";}
		}
	}
}
