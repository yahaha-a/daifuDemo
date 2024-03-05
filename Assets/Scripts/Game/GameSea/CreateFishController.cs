using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CreateFishController : ViewController, IController
	{
		public GameObject fishRoot;

		private List<string> _fishKey = new List<string>();

		private float _refreshInterval = 0f;

		private void Update()
		{
			if (_refreshInterval <= 0f)
			{
				var randomFishPrefab = this.SendQuery(new FindARandomFishPrefab());
				Instantiate(randomFishPrefab, fishRoot.transform);
				_refreshInterval = 5f;
			}
			else
			{
				_refreshInterval -= Time.deltaTime;
			}
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
