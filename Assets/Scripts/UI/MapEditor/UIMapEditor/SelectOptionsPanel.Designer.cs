/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class SelectOptionsPanel
	{
		[SerializeField] public RectTransform OptionRoot;
		[SerializeField] public OptionTemplete OptionTemplete;
		[SerializeField] public UnityEngine.UI.Button CancelButton;
		[SerializeField] public InputCreateNumberPanel InputCreateNumberPanel;

		public void Clear()
		{
			OptionRoot = null;
			OptionTemplete = null;
			CancelButton = null;
			InputCreateNumberPanel = null;
		}

		public override string ComponentName
		{
			get { return "SelectOptionsPanel";}
		}
	}
}
