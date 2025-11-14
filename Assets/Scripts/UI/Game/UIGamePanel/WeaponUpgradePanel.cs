/****************************************************************************
 * 2025.3 WXH
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace daifuDemo
{
	public partial class WeaponUpgradePanel : UIElement
	{
		private bool _ifShow = false;
		private float _duration = 0;
		private float _fadeDuration;
		
		private void Start()
		{
			Events.ShowUpgradeDetails.Register((time, details) =>
			{
				var color = Details.color;
				color.a = 1;
				Details.color = color;
				Details.text = details;
				_duration = time;
				_fadeDuration = _duration / 4;
				_ifShow = true;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (_ifShow)
			{
				if (_duration > 0)
				{
					Details.Show();
					_duration -= Time.deltaTime;
					
					if (_duration <= _fadeDuration)
					{
						var color = Details.color;
						color.a = _duration / _fadeDuration;
						Details.color = color;
					}
				}
				else
				{
					Details.Hide();
					_ifShow = false;
				}
			}
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}