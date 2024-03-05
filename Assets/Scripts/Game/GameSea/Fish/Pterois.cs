using UnityEngine;
using QFramework;

namespace daifuDemo
{
	public partial class Pterois : ViewController, IAggressiveFish
	{
		public string FishKey { get; set; }
		
		public FishState FishState { get; set; }
		
		public float ToggleDirectionTime { get; set; }
		
		public float RangeOfMovement { get; set; }

		public float SwimRate { get; set; }
		
		public float FrightenedSwimRate { get; set; }
		
		public float Damage { get; set; }
		
		public float PursuitSwimRate { get; set; }
		
		public float AttackInterval { get; set; }

		public Vector2 CurrentDirection { get; set; }

		public Vector3 StartPosition { get; set; }

		public float CurrentSwimRate { get; set; }

		public float CurrentToggleDirectionTime { get; set; }
		
		public float Hp { get; set; }


		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				FishState = FishState.Attack;
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
			
			var attackInterval = 0f;
			HitBox.OnTriggerStay2DEvent(other =>
			{
				if (attackInterval <= 0f)
				{
					if (other.CompareTag("Player"))
					{
						this.SendCommand(new PlayerIsHitCommand(Damage));
					}
					
					attackInterval = AttackInterval;
				}
				else
				{
					attackInterval -= Time.deltaTime;
				}
				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		private void Update()
		{
			Movement();
		}

		private void InitData()
		{
			FishKey = Config.PteroisKey;
			
			FishState = this.SendQuery(new FindFishState(FishKey));
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			RangeOfMovement = this.SendQuery(new FindFishRangeOfMovement(FishKey));
			Damage = this.SendQuery(new FindFishDamage(FishKey));
			PursuitSwimRate = this.SendQuery(new FindFishPursuitSwimRate(FishKey));
			Hp = this.SendQuery(new FindFishHp(FishKey));
			AttackInterval = this.SendQuery(new FindFishAttackInterval(FishKey));

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
			else if (FishState == FishState.Attack)
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;
				CurrentDirection = (playerPosition - transform.position).normalized;
				CurrentSwimRate = PursuitSwimRate;
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
		
		public void HitByFishFork()
		{
			FishState = FishState.Hit;

			ActionKit.OnUpdate.Register(() =>
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;

				if (Vector3.Distance(playerPosition, transform.position) <= 1f)
				{
					FishState = FishState.Caught;
					Events.CatchFish?.Trigger(this);
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
