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

		public Vector2 CurrentDirection { get; private set; }

		public Vector3 StartPosition { get; private set; }

		public float CurrentSwimRate { get; private set; }

		public float CurrentToggleDirectionTime { get; private set; }
		
		public float Hp { get; set; }

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
			InitData();
			
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
			Movement();
		}

		private void InitData()
		{
			FishKey = Config.NormalFishKey;
			
			FishState = this.SendQuery(new FindFishState(FishKey));
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			RangeOfMovement = this.SendQuery(new FindFishRangeOfMovement(FishKey));
			Hp = this.SendQuery(new FindFishHp(FishKey));

			CurrentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			StartPosition = transform.position;
			CurrentToggleDirectionTime = ToggleDirectionTime;
			CurrentSwimRate = SwimRate;
		}

		private void Movement()
		{
			if (Vector3.Distance(transform.position, StartPosition) >= RangeOfMovement)
			{
				CurrentDirection = -CurrentDirection;
			}

			if (FishState == FishState.Frightened)
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;
				CurrentDirection = (transform.position - playerPosition).normalized;
				CurrentSwimRate = FrightenedSwimRate;
			}
			else if (FishState == FishState.Swim)
			{
				CurrentSwimRate = SwimRate;
				CurrentToggleDirectionTime -= Time.deltaTime;
				if (CurrentToggleDirectionTime <= 0)
				{
					CurrentToggleDirectionTime = ToggleDirectionTime;
					CurrentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				}
			}
			else
			{
				CurrentSwimRate = 0;
				CurrentDirection = Vector2.zero;
			}
			
			var swimSpeed = CurrentSwimRate * CurrentDirection;
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
