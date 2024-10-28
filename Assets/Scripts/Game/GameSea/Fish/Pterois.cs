using Global;
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
		
		public float CoolDownTime { get; set; }

		public float Damage { get; set; }
		
		public float PursuitSwimRate { get; set; }
		
		public float AttackInterval { get; set; }
		
		public float AttackRange { get; set; }
		
		public Vector2 TargetDirection { get; set; }

		public Vector3 StartPosition { get; set; }
		
		public float Hp { get; set; }
		
		public float FleeHp { get; set; }

		public float StruggleTime { get; set; }

		public float VisualField { get; set; }

		public int Clicks { get; set; }
		
		public bool HitByBullet { get; set; }
		
		public Vector2 HitPosition { get; set; }
		
		public float ChargeTime { get; set; }

		public float CurrentCoolDownTime { get; set; }
		
		public float CurrentAttackInterval { get; set; }
		
		public Vector2 CurrentAttackTargetPosition { get; set; }
		
		public float CurrentStruggleTime { get; set; }
		
		public float CurrentSwimRate { get; set; }

		public float CurrentToggleDirectionTime { get; set; }
		
		public Vector2 CurrentDirection { get; set; }
		
		public float CurrentChargeTime { get; set; }

		public bool IfAttack { get; set; } = false;

		public bool CanSwim { get; set; } = true;

		public bool HitByFork { get; set; } = false;

		public bool DiscoverPlayer { get; set; } = false;

		public bool IfStruggle { get; set; } = false;

		public bool IfCharge { get; set; } = false;

		private PteroisFishBehaviorTree<Pterois> _bt;

		private Player _player;

		private IPlayerModel _playerModel;

		private IUtils _utils;

		private void Start()
		{
			InitData();
			
			_bt = new PteroisFishBehaviorTree<Pterois>(this);
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
				else if (other.CompareTag("PlayerHurtBox"))
				{
					if (IfAttack)
					{
						this.SendCommand(new PlayerIsHitCommand(Damage));
					}
				}
				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		private void Update()
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

			if (CurrentDirection.x < 0)
			{
				Icon.GetComponent<SpriteRenderer>().flipX = false;
			}
			else
			{
				Icon.GetComponent<SpriteRenderer>().flipX = true;
			}

			if (IfStruggle)
			{
				CurrentStruggleTime -= Time.deltaTime;
			}

			if (HitByBullet)
			{
				CurrentCoolDownTime -= Time.deltaTime;

				if (CurrentCoolDownTime <= 0)
				{
					CurrentCoolDownTime = CoolDownTime;
					HitByBullet = false;
				}
			}

			if (Vector2.Distance(transform.position, _playerModel.CurrentPosition.Value) > VisualField)
			{
				DiscoverPlayer = false;
			}
			else
			{
				DiscoverPlayer = true;
			}

			if (!IfAttack)
			{
				CurrentAttackInterval -= Time.deltaTime;
			}

			if (IfAttack)
			{
				if (Vector2.Distance(transform.position, CurrentAttackTargetPosition) <= 0.1f)
				{
					IfAttack = false;
					CanSwim = true;
					IfCharge = false;
				}
				else
				{
					var position = transform.position;
					transform.position = Vector3.Lerp(position, CurrentAttackTargetPosition,
						1 - Mathf.Exp(-Time.deltaTime * 10));
				}
			}

			if (IfCharge)
			{
				CurrentChargeTime -= Time.deltaTime;
			}
		}

		private void InitData()
		{
			FishKey = Config.AggressiveFishKey;
			
			_player = FindObjectOfType<Player>().GetComponent<Player>();
			_playerModel = this.GetModel<IPlayerModel>();
			_utils = this.GetUtility<IUtils>();
			
			Icon.sprite = _utils.AdjustSprite(this.SendQuery(new FindFishIcon(FishKey)));
			FishState = this.SendQuery(new FindFishState(FishKey));
			ToggleDirectionTime = this.SendQuery(new FindFishToggleDirectionTime(FishKey));
			SwimRate = this.SendQuery(new FindFishSwimRate(FishKey));
			FrightenedSwimRate = this.SendQuery(new FindFishFrightenedSwimRate(FishKey));
			CoolDownTime = this.SendQuery(new FindFishCoolDownTime(FishKey));
			Damage = this.SendQuery(new FindFishDamage(FishKey));
			PursuitSwimRate = this.SendQuery(new FindFishPursuitSwimRate(FishKey));
			Hp = this.SendQuery(new FindFishHp(FishKey));
			FleeHp = this.SendQuery(new FindFishFleeHp(FishKey));
			StruggleTime = this.SendQuery(new FindFishStruggleTime(FishKey));
			VisualField = this.SendQuery(new FindFishVisualField(FishKey));
			Clicks = this.SendQuery(new FindFishClicks(FishKey));
			AttackInterval = this.SendQuery(new FindFishAttackInterval(FishKey));
			AttackRange = this.SendQuery(new FindFishAttackRange(FishKey));
			ChargeTime = this.SendQuery(new FindFishChargeTime(FishKey));

			CurrentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			CurrentToggleDirectionTime = ToggleDirectionTime;
			CurrentSwimRate = SwimRate;
			CurrentCoolDownTime = CoolDownTime;
			CurrentStruggleTime = StruggleTime;
			CurrentAttackInterval = AttackInterval;
			CurrentChargeTime = ChargeTime;
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
