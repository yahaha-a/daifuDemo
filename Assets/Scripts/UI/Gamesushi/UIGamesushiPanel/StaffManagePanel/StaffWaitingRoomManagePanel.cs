/****************************************************************************
 * 2024.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class StaffWaitingRoomManagePanel : UIElement, IController
	{
		private IStaffSystem _staffSystem;

		private IUIGamesushiPanelModel _uiGamesushiPanelModel;

		private List<WaitingStaffTemplate> _waitingStaffs = new List<WaitingStaffTemplate>();
		
		private void Awake()
		{
			_staffSystem = this.GetSystem<IStaffSystem>();

			_uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

			_uiGamesushiPanelModel.IfReplaceConfirmPanelShow.Register(value =>
			{
				if (value)
				{
					ReplaceConfirmPanel.Show();
				}
				else
				{
					ReplaceConfirmPanel.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnEnable()
		{
			foreach (var (staffKey, staffItemInfo) in _staffSystem.CurrentOwnStaffItems)
			{
				if (staffItemInfo.State == StaffState.Free)
				{
					WaitingStaffTemplate.InstantiateWithParent(WaitingStaffRoot).Self(self =>
					{
						self.CurrentStaffItemInfo = staffItemInfo;
						self.Show();
						_waitingStaffs.Add(self);
					});
				}
			}

			AdjustContentHeight();
		}

		private void OnDisable()
		{
			foreach (var waitingStaffTemplate in _waitingStaffs)
			{
				waitingStaffTemplate.gameObject.DestroySelf();
			}
			_waitingStaffs.Clear();
		}
		
		void AdjustContentHeight()
		{
			float totalHeight = 0f;

			foreach (Transform child in WaitingStaffRoot)
			{
				if (child.gameObject.activeSelf)
				{
					RectTransform childRect = child.GetComponent<RectTransform>();
					totalHeight += childRect.sizeDelta.y + 25f;
				}
			}

			WaitingStaffRoot.sizeDelta = new Vector2(WaitingStaffRoot.sizeDelta.x, totalHeight + 25f);
		}

		protected override void OnBeforeDestroy()
		{
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}