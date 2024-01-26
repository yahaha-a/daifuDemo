using System;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace daifuDemo
{
	public enum FishState
	{
		SWIM,
		FRIGHTENED,
		HIT,
		CAUGHT
	}
	
	public partial class NormalFish : ViewController
	{
		private FishState _fishState = FishState.SWIM;
		
		private float _toggleDirectionTime = 3.0f;

		private float _swimRate = 2.0f;

		private Vector2 _direction;

		private Vector3 _startPosition;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				_fishState = FishState.FRIGHTENED;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				_fishState = FishState.SWIM;
			}
		}

		private void Awake()
		{
			_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			_startPosition = transform.position;

			FishForkHead.HitFish.Register(() =>
			{
				HitByFishFork();
                
				if (_fishState == FishState.CAUGHT)
				{
					FishForkHead.CatchFish?.Trigger();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (Vector3.Distance(transform.position, _startPosition) >= 10f)
			{
				_direction = -_direction;
			}

			if (_fishState == FishState.FRIGHTENED)
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;
				_direction = (transform.position - playerPosition).normalized;
				_swimRate = 5.0f;
			}
			else if (_fishState == FishState.SWIM)
			{
				_swimRate = 3.0f;
				_toggleDirectionTime -= Time.deltaTime;
				if (_toggleDirectionTime <= 0)
				{
					_toggleDirectionTime = 3.0f;
					_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				}
			}
			else
			{
				_swimRate = 0;
				_direction = Vector2.zero;
			}
			
			var swimSpeed = _swimRate * _direction;
			var position = transform.position;
			transform.position = new Vector3(position.x + swimSpeed.x * Time.deltaTime,
				position.y + swimSpeed.y * Time.deltaTime, position.z);
		}

		private void HitByFishFork()
		{
			_fishState = FishState.HIT;

			ActionKit.OnUpdate.Register(() =>
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;

				if (Vector3.Distance(playerPosition, transform.position) <= 1f)
				{
					_fishState = FishState.CAUGHT;
					Player._numberOfFish.Value += 1;
					gameObject.DestroySelf();
				}
				else
				{
					var position = transform.position;
					transform.position = Vector3.Lerp(position, playerPosition, 1 - Mathf.Exp(-Time.deltaTime * 30));
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
	}
}
