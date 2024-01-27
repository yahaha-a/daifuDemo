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
		
		private void Start()
		{
			var randomFishPrefab = this.SendQuery(new FindARandomFishPrefab());
			Instantiate(randomFishPrefab, fishRoot.transform);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
