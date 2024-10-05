using System;
using MapEditor;
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
