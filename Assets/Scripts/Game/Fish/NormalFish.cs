using System;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Random = UnityEngine.Random; 

namespace daifuDemo
{
	public partial class NormalFish : ViewController, IFish
	{
		public string FishKey { get; private set; }
		
		public FishState FishState { get; private set; }
		
		public float ToggleDirectionTime { get; private set; }
		
		public float RangeOfMovement { get; private set; }

		public float SwimRate { get; private set; }
		
		public float FrightenedSwimRate { get; private set; }

		private Vector2 _direction;

		private Vector3 _startPosition;

		private float _currentSwimRate;

		private float _currentToggleDirectionTime;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				FishState = FishState.Frightened;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				FishState = FishState.Swim;
			}
		}

		private void Start()
		{
			FishKey = Config.NormalFishKey;
			
			FishState = this.SendQuery(new FindFishState(FishKey));
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			RangeOfMovement = this.SendQuery(new FindFishRangeOfMovement(FishKey));

			_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			_startPosition = transform.position;
			_currentToggleDirectionTime = ToggleDirectionTime;
			_currentSwimRate = SwimRate;
			
			FishForkHead.HitFish.Register(() =>
			{
				HitByFishFork();
                
				if (FishState == FishState.Caught)
				{
					FishForkHead.CatchFish?.Trigger();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void Update()
		{
			if (Vector3.Distance(transform.position, _startPosition) >= RangeOfMovement)
			{
				_direction = -_direction;
			}

			if (FishState == FishState.Frightened)
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;
				_direction = (transform.position - playerPosition).normalized;
				_currentSwimRate = FrightenedSwimRate;
			}
			else if (FishState == FishState.Swim)
			{
				_currentSwimRate = SwimRate;
				_currentToggleDirectionTime -= Time.deltaTime;
				if (_currentToggleDirectionTime <= 0)
				{
					_currentToggleDirectionTime = ToggleDirectionTime;
					_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				}
			}
			else
			{
				_currentSwimRate = 0;
				_direction = Vector2.zero;
			}
			
			var swimSpeed = _currentSwimRate * _direction;
			var position = transform.position;
			transform.position = new Vector3(position.x + swimSpeed.x * Time.deltaTime,
				position.y + swimSpeed.y * Time.deltaTime, position.z);
		}

		private void HitByFishFork()
		{
			FishState = FishState.Hit;

			ActionKit.OnUpdate.Register(() =>
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;

				if (Vector3.Distance(playerPosition, transform.position) <= 1f)
				{
					FishState = FishState.Caught;
					this.SendCommand<FishCountAddOneCommand>();
					gameObject.DestroySelf();
				}
				else
				{
					var position = transform.position;
					transform.position = Vector3.Lerp(position, playerPosition, 1 - Mathf.Exp(-Time.deltaTime * 30));
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
