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
		
		public float CurrentAttackInterval { get; set; }
		
		public float AttackRange { get; set; }

		public Vector2 CurrentDirection { get; set; }

		public Vector3 StartPosition { get; set; }

		public float CurrentSwimRate { get; set; }

		public float CurrentToggleDirectionTime { get; set; }
		
		public float Hp { get; set; }
		
		public float FleeHp { get; set; }

		public float StruggleTime { get; set; }
		
		public float CurrentStruggleTime { get; set; }
		
		public int Clicks { get; set; }
		
		public bool CanSwim { get; set; }

		public bool HitByFork { get; set; }
		
		public bool DiscoverPlayer { get; set; }
		
		public bool HitByBullet { get; set; }
		
		private PteroisFishBehaviorTree<Pterois> _bt;

		private Player _player;


		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				DiscoverPlayer = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				DiscoverPlayer = false;
			}
		}
		
		private void Start()
		{
			InitData();
			
			_bt = new PteroisFishBehaviorTree<Pterois>(this, _player);
			_bt.Init();
		}
		
		private void Update()
		{
			_bt.Tick();

			if (CanSwim)
			{
				var swimSpeed = CurrentSwimRate * CurrentDirection;
				var position = transform.position;
				transform.position = new Vector3(position.x + swimSpeed.x * Time.deltaTime,
					position.y + swimSpeed.y * Time.deltaTime, position.z);
			}
		}

		private void InitData()
		{
			FishKey = Config.PteroisKey;
			
			Icon.sprite = this.SendQuery(new FindFishIcon(FishKey));
			
			_player = FindObjectOfType<Player>().GetComponent<Player>();
			
			FishState = this.SendQuery(new FindFishState(FishKey));
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			RangeOfMovement = this.SendQuery(new FindFishRangeOfMovement(FishKey));
			Damage = this.SendQuery(new FindFishDamage(FishKey));
			PursuitSwimRate = this.SendQuery(new FindFishPursuitSwimRate(FishKey));
			Hp = this.SendQuery(new FindFishHp(FishKey));
			FleeHp = this.SendQuery(new FindFishFleeHp(FishKey));
			StruggleTime = this.SendQuery(new FindFishStruggleTime(FishKey));
			Clicks = this.SendQuery(new FindFishClicks(FishKey));
			AttackInterval = this.SendQuery(new FindFishAttackInterval(FishKey));
			AttackRange = this.SendQuery(new FindFishAttackRange(FishKey));

			CurrentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			StartPosition = transform.position;
			CurrentToggleDirectionTime = ToggleDirectionTime;
			CurrentSwimRate = SwimRate;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
