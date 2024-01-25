using System;
using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public enum FishForkState
	{
		READY,
		AIM,
		REVOLVE,
		LAUNCH
	}
	
	public partial class FishFork : ViewController
	{
		private float _rotationRate = 50f;

		public static FishForkState _fishForkState = FishForkState.READY;

		private void Update()
		{
			TransitionState();
			TakeAction();
		}

		private void TransitionState()
		{
			switch (_fishForkState)
			{
				case FishForkState.READY:
					if (Input.GetKeyDown(KeyCode.I))
					{
						_fishForkState = FishForkState.AIM;
					}
					break;
				case FishForkState.AIM:
					if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
					{
						_fishForkState = FishForkState.REVOLVE;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkState = FishForkState.READY;
					}
					else if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkState = FishForkState.LAUNCH;
					}
					break;
				case FishForkState.REVOLVE:
					if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkState = FishForkState.LAUNCH;
					}
					else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
					{
						_fishForkState = FishForkState.AIM;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkState = FishForkState.READY;
					}
					break;
				case FishForkState.LAUNCH:
					_fishForkState = FishForkState.READY;
					break;
			}
		}

		private void TakeAction()
		{
			switch (_fishForkState)
			{
				case FishForkState.READY:
					break;
				case FishForkState.AIM:
					break;
				case FishForkState.REVOLVE:
					if (Input.GetKey(KeyCode.Z))
					{
						if (transform.eulerAngles.z < 70f || transform.eulerAngles.z > 289f)
						{
							var rotationAmount = _rotationRate * Time.deltaTime;
							transform.Rotate(new Vector3(0, 0, rotationAmount));
						}
					}
					else if (Input.GetKey(KeyCode.C))
					{
						if (transform.eulerAngles.z < 71f || transform.eulerAngles.z > 290f)
						{
							var rotationAmount = -_rotationRate * Time.deltaTime;
							transform.Rotate(new Vector3(0, 0, rotationAmount));
						}
					}
					break;
				case FishForkState.LAUNCH:
					FishForkHeadTemplate.InstantiateWithParent(this)
						.Self(self =>
						{
							self.GetComponent<FishForkHead>().originTransform = transform;
							self.Show();
						});
					break;
			}
		}
	}
}
