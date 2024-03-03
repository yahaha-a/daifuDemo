/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class StaffRestaurantManagePanel
	{
		[SerializeField] public RectTransform KitchenStaffRoot;
		[SerializeField] public RectTransform LobbyStaffRoot;
		[SerializeField] public RestaurantStaffTemplate RestaurantStaffTemplate;

		public void Clear()
		{
			KitchenStaffRoot = null;
			LobbyStaffRoot = null;
			RestaurantStaffTemplate = null;
		}

		public override string ComponentName
		{
			get { return "StaffRestaurantManagePanel";}
		}
	}
}
