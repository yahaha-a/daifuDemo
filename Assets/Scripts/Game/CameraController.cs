using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CameraController : ViewController
	{
		private Transform _mPlayerTransform;

		private void Awake()
		{
			_mPlayerTransform = FindObjectOfType<Player>().transform;
		}

		private void Update()
		{
			if (_mPlayerTransform != null)
			{
				var position = transform.position;
				var cameraTargetPosition = Vector2.Lerp(position, _mPlayerTransform.position,
					1 - Mathf.Exp(-Time.deltaTime * 10));
				position = new Vector3(cameraTargetPosition.x, cameraTargetPosition.y, position.z);
				transform.position = position;
			}
		}
	}
}
