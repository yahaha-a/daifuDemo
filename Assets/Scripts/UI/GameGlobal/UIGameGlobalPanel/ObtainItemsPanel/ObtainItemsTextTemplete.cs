/****************************************************************************
 * 2024.10 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class ObtainItemsTextTemplete : UIElement, IController
	{
		public string obtainItemName;
		private Text textComponent;

		private IUIGameGlobalPanelModel _uiGameGlobalPanelModel;
		
		float timeElapsed = 0f;
		float duration = 2f;

		private void Start()
		{
			_uiGameGlobalPanelModel = this.GetModel<IUIGameGlobalPanelModel>();
			
			textComponent = GetComponent<Text>();
			textComponent.text = obtainItemName;
		}
		
		private void Update()
		{
			if (timeElapsed < duration)
			{
				timeElapsed += Time.deltaTime;
				float t = timeElapsed / duration;
        
				float alpha = Mathf.Lerp(1f, 0f, t);
        
				textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
			}

			if (timeElapsed >= duration)
			{
				_uiGameGlobalPanelModel.CurrentShowObtainItemsCount.Value--;
				transform.gameObject.DestroySelf();
			}
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