/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class UITaskSelectPanel
	{
		[SerializeField] public RectTransform TaskListRoot;
		[SerializeField] public TaskTemplete TaskTemplete;
		[SerializeField] public RectTransform NullTask;
		[SerializeField] public RectTransform TaskMessage;
		[SerializeField] public UnityEngine.UI.Button TaskButton;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Describe;
		[SerializeField] public RectTransform ProcessRoot;
		[SerializeField] public MessageProcessTemplete MessageProcessTemplete;
		[SerializeField] public UnityEngine.UI.Button CloseButton;

		public void Clear()
		{
			TaskListRoot = null;
			TaskTemplete = null;
			NullTask = null;
			TaskMessage = null;
			TaskButton = null;
			Name = null;
			Describe = null;
			ProcessRoot = null;
			MessageProcessTemplete = null;
			CloseButton = null;
		}

		public override string ComponentName
		{
			get { return "UITaskSelectPanel";}
		}
	}
}
