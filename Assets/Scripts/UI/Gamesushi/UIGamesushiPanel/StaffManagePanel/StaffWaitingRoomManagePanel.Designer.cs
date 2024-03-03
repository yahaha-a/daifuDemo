/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class StaffWaitingRoomManagePanel
	{
		[SerializeField] public RectTransform WaitingStaffRoot;
		[SerializeField] public WaitingStaffTemplate WaitingStaffTemplate;
		[SerializeField] public ReplaceConfirmPanel ReplaceConfirmPanel;

		public void Clear()
		{
			WaitingStaffRoot = null;
			WaitingStaffTemplate = null;
			ReplaceConfirmPanel = null;
		}

		public override string ComponentName
		{
			get { return "StaffWaitingRoomManagePanel";}
		}
	}
}
