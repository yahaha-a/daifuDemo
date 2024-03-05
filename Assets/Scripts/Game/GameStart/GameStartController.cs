using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class GameStartController : ViewController
	{
		private void Start()
		{
			UIKit.OpenPanel<UIGameStartPanel>();
		}
	}
}
