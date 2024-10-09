using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class CameraController : ViewController
	{
		private Transform _barrierRoot;
		
		private Transform _mPlayerTransform;
		
		private float minX, maxX, minY, maxY;
		private float cameraHalfWidth, cameraHalfHeight;

		private void Awake()
		{
			Events.MapInitializationComplete.Register(() =>
			{
				_mPlayerTransform = FindObjectOfType<Player>().transform;
				_barrierRoot = GameObject.FindGameObjectWithTag("BarrierRoot").transform;
				CalculateWallBounds();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Start()
		{
			cameraHalfHeight = Camera.main.orthographicSize;
			cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;
		}

		private void Update()
		{
			if (_mPlayerTransform != null)
			{
				var position = transform.position;

				var cameraTargetPosition = Vector2.Lerp(position, _mPlayerTransform.position,
					1 - Mathf.Exp(-Time.deltaTime * 10));
				position = new Vector3(cameraTargetPosition.x, cameraTargetPosition.y, position.z);

				position.x = Mathf.Clamp(position.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);
				position.y = Mathf.Clamp(position.y, minY + cameraHalfHeight, maxY - cameraHalfHeight);

				transform.position = position;
			}
		}
		
		void CalculateWallBounds()
		{
			minX = 0;
			maxX = 0;
			minY = 0;
			maxY = 0;

			foreach (Transform wallBlock in _barrierRoot)
			{
				Vector3 blockPosition = wallBlock.position;

				if (blockPosition.x < minX) minX = blockPosition.x;
				if (blockPosition.x > maxX) maxX = blockPosition.x;
				if (blockPosition.y < minY) minY = blockPosition.y;
				if (blockPosition.y > maxY) maxY = blockPosition.y;
			}

			minX -= 0.5f;
			minY -= 0.5f;
			maxX += 0.5f;
			maxY += 0.5f;
		}
	}
}
