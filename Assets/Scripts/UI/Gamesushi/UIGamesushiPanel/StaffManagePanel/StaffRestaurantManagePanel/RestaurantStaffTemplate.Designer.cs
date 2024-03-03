/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class RestaurantStaffTemplate
	{
		[SerializeField] public UnityEngine.UI.Button AddCookerButton;
		[SerializeField] public UnityEngine.UI.Button AddWaiterButton;
		[SerializeField] public RectTransform StaffDetail;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public UnityEngine.UI.Text Rank;
		[SerializeField] public UnityEngine.UI.Text Name;
		[SerializeField] public UnityEngine.UI.Text WalkSpeed;
		[SerializeField] public UnityEngine.UI.Text CookSpeed;

		public void Clear()
		{
			AddCookerButton = null;
			AddWaiterButton = null;
			StaffDetail = null;
			Icon = null;
			Rank = null;
			Name = null;
			WalkSpeed = null;
			CookSpeed = null;
		}

		public override string ComponentName
		{
			get { return "RestaurantStaffTemplate";}
		}
	}
}
