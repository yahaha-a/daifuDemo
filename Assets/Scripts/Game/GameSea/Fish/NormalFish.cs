using System;
using System.Numerics;
using Global;
using UnityEngine;
using QFramework;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace daifuDemo
{
	public partial class NormalFish : ViewController, IFish
	{
		public FishState State { get; set; }
		
		public string FishKey { get; set; }
		
		public float ToggleDirectionTime { get; set; }
		
		public float RangeOfMovement { get; set; }

		public float SwimRate { get; set; }
		
		public float FrightenedSwimRate { get; set; }
		
		public float CoolDownTime { get; set; }
		
		public float CurrentCoolDownTime { get; set; }

		public Vector2 CurrentDirection { get; set; }
		
		public Vector2 TargetDirection { get; set; }

		public Vector3 StartPosition { get; set; }

		public float CurrentSwimRate { get; set; }

		public float CurrentToggleDirectionTime { get; set; }
		
		public float Hp { get; set; }
		
		public float FleeHp { get; set; }

		public float StruggleTime { get; set; }
		
		public float CurrentStruggleTime { get; set; }
		
		public bool IfStruggle { get; set; }

		public float VisualField { get; set; }
		
		public int Clicks { get; set; }
		
		public bool CanSwim { get; set; }
		
		public bool HitByFork { get; set; }
		
		public bool DiscoverPlayer { get; set; }
		
		public bool HitByBullet { get; set; }
		
		public Vector2 HitPosition { get; set; }

		private NormalFishBehaviorTree<NormalFish> _bt;

		private Player _player;

		private IPlayerModel _playerModel;

		private IUtils _utils;
		
		private void Start()
		{
			InitData();
			
			_bt = new NormalFishBehaviorTree<NormalFish>(this, _player);
			_bt.Init();

			HitBox.OnTriggerEnter2DEvent(other =>
			{
				if (other.CompareTag("BarrierBox"))
				{
					CurrentDirection = -CurrentDirection;

					Vector3 baseEscapeDirection = (transform.position - other.gameObject.transform.position).normalized;
					float randomOffsetX = Random.Range(-0.3f, 0.3f);
					float randomOffsetY = Random.Range(-0.3f, 0.3f);
					Vector3 randomOffset = new Vector3(randomOffsetX, randomOffsetY, 0);
					CurrentDirection = (baseEscapeDirection + randomOffset).normalized;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		public void Update()
		{
			_bt.Tick();

			if (CanSwim)
			{
				CurrentToggleDirectionTime -= Time.deltaTime;
				
				var swimSpeed = CurrentSwimRate * CurrentDirection;
				var position = transform.position;
				transform.position = new Vector3(position.x + swimSpeed.x * Time.deltaTime,
					position.y + swimSpeed.y * Time.deltaTime, position.z);
			}

			if (State == FishState.Struggle)
			{
				CurrentStruggleTime -= Time.deltaTime;
			}

			if (Vector2.Distance(transform.position, _playerModel.CurrentPosition.Value) > VisualField)
			{
				DiscoverPlayer = false;
			}
			else
			{
				DiscoverPlayer = true;
			}
		}

		private void InitData()
		{
			FishKey = Config.NormalFishKey;
			
			_player = FindObjectOfType<Player>().GetComponent<Player>();
			_playerModel = this.GetModel<IPlayerModel>();
			_utils = this.GetUtility<IUtils>();

			Icon.sprite = _utils.AdjustSprite(this.SendQuery(new FindFishIcon(FishKey)));
			
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			CoolDownTime = this.SendQuery(new FindFishCoolDownTime(FishKey));
			Hp = this.SendQuery(new FindFishHp(FishKey));
			StruggleTime = this.SendQuery(new FindFishStruggleTime(FishKey));
			VisualField = this.SendQuery(new FindFishVisualField(FishKey));
			Clicks = this.SendQuery(new FindFishClicks(FishKey));

			CurrentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			CurrentToggleDirectionTime = ToggleDirectionTime;
			CurrentSwimRate = SwimRate;
			CurrentStruggleTime = StruggleTime;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
