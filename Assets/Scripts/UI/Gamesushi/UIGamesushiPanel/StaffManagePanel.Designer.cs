/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class StaffManagePanel
	{
		[SerializeField] public StaffRestaurantManagePanel StaffRestaurantManagePanel;
		[SerializeField] public StaffWaitingRoomManagePanel StaffWaitingRoomManagePanel;
		[SerializeField] public UnityEngine.UI.Button RestaurantButton;
		[SerializeField] public UnityEngine.UI.Button WaitingRoomButton;

		public void Clear()
		{
			StaffRestaurantManagePanel = null;
			StaffWaitingRoomManagePanel = null;
			RestaurantButton = null;
			WaitingRoomButton = null;
		}

		public override string ComponentName
		{
			get { return "StaffManagePanel";}
		}
	}
}
