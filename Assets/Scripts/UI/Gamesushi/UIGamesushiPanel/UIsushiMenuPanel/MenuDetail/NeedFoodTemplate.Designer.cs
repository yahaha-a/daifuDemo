/****************************************************************************
 * 2024.2 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class NeedFoodTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text OwnAndNeedAmount;

		public void Clear()
		{
			Icon = null;
			Name = null;
			OwnAndNeedAmount = null;
		}

		public override string ComponentName
		{
			get { return "NeedFoodTemplate";}
		}
	}
}
