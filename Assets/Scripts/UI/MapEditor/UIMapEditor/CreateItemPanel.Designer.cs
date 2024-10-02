/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class CreateItemPanel
	{
		[SerializeField] public RectTransform CreateItemRoot;
		[SerializeField] public CreateItemTemplete CreateItemTemplete;

		public void Clear()
		{
			CreateItemRoot = null;
			CreateItemTemplete = null;
		}

		public override string ComponentName
		{
			get { return "CreateItemPanel";}
		}
	}
}
