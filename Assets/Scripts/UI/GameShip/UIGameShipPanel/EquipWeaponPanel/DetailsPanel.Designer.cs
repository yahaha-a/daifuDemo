/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class DetailsPanel
	{
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Button Upgrade;
		[SerializeField] public SelectBulletTypePanel SelectBulletTypePanel;
		[SerializeField] public UnityEngine.UI.Button Equip;
		[SerializeField] public UnityEngine.UI.Text EquipText;

		public void Clear()
		{
			Name = null;
			Rank = null;
			Upgrade = null;
			SelectBulletTypePanel = null;
			Equip = null;
			EquipText = null;
		}

		public override string ComponentName
		{
			get { return "DetailsPanel";}
		}
	}
}
