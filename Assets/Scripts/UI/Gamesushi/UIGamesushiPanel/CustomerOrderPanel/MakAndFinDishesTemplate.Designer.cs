/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class MakAndFinDishesTemplate
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Slider Slider;

		public void Clear()
		{
			Icon = null;
			Name = null;
			Slider = null;
		}

		public override string ComponentName
		{
			get { return "MakAndFinDishesTemplate";}
		}
	}
}
