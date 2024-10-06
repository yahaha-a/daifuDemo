/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UITaskDisplayPanel
	{
		[SerializeField] public RectTransform CurrentTask;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Describe;
		[SerializeField] public RectTransform ProcessRoot;
		[SerializeField] public ProcessTemplete ProcessTemplete;
		[SerializeField] public RectTransform NullTask;

		public void Clear()
		{
			CurrentTask = null;
			Name = null;
			Describe = null;
			ProcessRoot = null;
			ProcessTemplete = null;
			NullTask = null;
		}

		public override string ComponentName
		{
			get { return "UITaskDisplayPanel";}
		}
	}
}
