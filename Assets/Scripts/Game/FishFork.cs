using System;
using UnityEngine;
using QFramework;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace daifuDemo
{
	public enum FishForkState
	{
		Ready,
		Aim,
		Revolve,
		Launch
	}
	
	public partial class FishFork : ViewController
	{
		private float _rotationRate = 50f;

		public FishForkState _fishForkState = FishForkState.Ready;

		public bool _ifLeft = false;

		public GameObject FlyerRoot;

		private void Update()
		{
			TransitionState();
			TakeAction();
		}

		private void TransitionState()
		{
			switch (_fishForkState)
			{
				case FishForkState.Ready:
					if (Input.GetKeyDown(KeyCode.I))
					{
						_fishForkState = FishForkState.Aim;
					}
					break;
				case FishForkState.Aim:
					if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
					{
						_fishForkState = FishForkState.Revolve;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkState = FishForkState.Ready;
					}
					else if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkState = FishForkState.Launch;
					}
					break;
				case FishForkState.Revolve:
					if (Input.GetKeyDown(KeyCode.J))
					{
						_fishForkState = FishForkState.Launch;
					}
					else if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
					{
						_fishForkState = FishForkState.Aim;
					}
					else if (Input.GetKeyUp(KeyCode.I))
					{
						_fishForkState = FishForkState.Ready;
					}
					break;
				case FishForkState.Launch:
					_fishForkState = FishForkState.Ready;
					break;
			}
		}

		private void TakeAction()
		{
			switch (_fishForkState)
			{
				case FishForkState.Ready:
					break;
				case FishForkState.Aim:
					break;
				case FishForkState.Revolve:
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
				case FishForkState.Launch:
					FishForkHeadTemplate.InstantiateWithParent(this)
						.Self(self =>
						{
							if (_ifLeft)
							{
								self.GetComponent<FishForkHead>().Direction = -1;
							}
							else
							{
								self.GetComponent<FishForkHead>().Direction = 1;
							}
							self.parent = FlyerRoot.transform;
							self.Show();
						});
					break;
			}
		}
	}
}
