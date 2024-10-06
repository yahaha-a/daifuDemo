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
	public partial class ObtainItemsIconTemplete : UIElement
	{
		public Sprite icon;
		
		float timeElapsed = 0f;
		float duration = 0.8f;
		Vector2 start;
		Vector2 end = new Vector2(960, 0);

		private void Start()
		{
			start = transform.position;
			this.GetComponent<Image>().sprite = icon;
		}

		private void Update()
		{
			if (timeElapsed < duration)
			{
				timeElapsed += Time.deltaTime;
				float t = timeElapsed / duration;
        
				float speedFactor = Mathf.Pow(t, 2);

				transform.position = Vector3.Lerp(start, end, speedFactor);
			}

			if (timeElapsed >= duration)
			{
				transform.gameObject.DestroySelf();
			}
		}


		protected override void OnBeforeDestroy()
		{
		}
	}
}