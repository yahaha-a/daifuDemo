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
		public string FishKey { get; set; }
		
		public float ToggleDirectionTime { get; set; }
		
		public float RangeOfMovement { get; set; }

		public float SwimRate { get; set; }
		
		public float FrightenedSwimRate { get; set; }

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

		private NormalFishBehaviorTree<NormalFish> _bt;

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
			
			_bt = new NormalFishBehaviorTree<NormalFish>(this, _player);
			_bt.Init();
		}
		
		public void Update()
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
			FishKey = Config.NormalFishKey;
			
			_player = FindObjectOfType<Player>().GetComponent<Player>();

			Icon.sprite = this.SendQuery(new FindFishIcon(FishKey));
			
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			RangeOfMovement = this.SendQuery(new FindFishRangeOfMovement(FishKey));
			Hp = this.SendQuery(new FindFishHp(FishKey));
			StruggleTime = this.SendQuery(new FindFishStruggleTime(FishKey));
			Clicks = this.SendQuery(new FindFishClicks(FishKey));

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
