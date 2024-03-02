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
	public partial class MakAndFinDishesTemplate : UIElement
	{
		public float MakeNeedTime { get; set; }

		private float _currentMakeTime = 0;

		private float _progress;

		private void Update()
		{
			if (_currentMakeTime < MakeNeedTime)
			{
				_currentMakeTime += Time.deltaTime;

				_progress = _currentMakeTime / MakeNeedTime;
			}

			Slider.value = _progress;
		}

		protected override void OnBeforeDestroy()
		{
		}
	}
}