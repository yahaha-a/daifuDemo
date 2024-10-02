/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace MapEditor
{
	public partial class CreateItemTemplete
	{
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Name;

		public void Clear()
		{
			Icon = null;
			Name = null;
		}

		public override string ComponentName
		{
			get { return "CreateItemTemplete";}
		}
	}
}
