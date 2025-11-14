using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum PlayState
    {
        Swim,
        UsingFishFork,
        UsingGun,
        CatchFish,
        Attack,
        OpeningTreasureChests,
        PickingUp
    }
    
    public class PlayerFsm : AbstractFsm<PlayState>, IController
    {
        private Player _player;
        private IPlayerModel _playerModel;
        private IFishForkModel _fishForkModel;
        private IGunModel _gunModel;
        private IUIGamePanelModel _uiGamePanelModel;

        public PlayerFsm(Player player)
        {
            _player = player;
        }
        
        public override void Init()
        {
            _playerModel = this.GetModel<IPlayerModel>();
            _fishForkModel = this.GetModel<IFishForkModel>();
            _gunModel = this.GetModel<IGunModel>();
            _uiGamePanelModel = this.GetModel<IUIGamePanelModel>();

            AddStates(new State<PlayState>()
                .WithKey(PlayState.Swim)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.Swim;
                })
                .WithOnExit(() =>
                {
                    _player.mRigidbody2D.linearVelocity = Vector2.zero;
                })
                .WithOnTick(() =>
                {
                    swim();
                }));
            
            AddStates(new State<PlayState>()
                .WithKey(PlayState.UsingFishFork)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.UsingFishFork;
                })
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<PlayState>()
                .WithKey(PlayState.UsingGun)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.UsingGun;
                })
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<PlayState>()
                .WithKey(PlayState.CatchFish)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.CatchFish;
                    _uiGamePanelModel.CurrentCounterPanelState.Value = CounterPanelState.CatchFish;
                })
                .WithOnExit(() =>
                {
                    _uiGamePanelModel.CurrentCounterPanelState.Value = CounterPanelState.Hide;
                })
                .WithOnTick(() =>
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _playerModel.FishingChallengeClicks.Value++;
                    }
                    _uiGamePanelModel.CurrentCounter.Value = (float)_playerModel.FishingChallengeClicks.Value /
                                                             (float)_playerModel.MaxFishingChallengeClicks.Value;
                }));
            
            AddStates(new State<PlayState>()
                .WithKey(PlayState.Attack)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.Attack;
                    Events.Attack?.Trigger();
                })
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<PlayState>()
                .WithKey(PlayState.OpeningTreasureChests)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.OpeningTreasureChests;
                    _uiGamePanelModel.CurrentCounterPanelState.Value = CounterPanelState.OpenTreasure;
                })
                .WithOnExit(() =>
                {
                    _playerModel.OpenChestSeconds.Value = 0;
                    _uiGamePanelModel.CurrentCounterPanelState.Value = CounterPanelState.Hide;
                })
                .WithOnTick(() =>
                {
                    _playerModel.OpenChestSeconds.Value += Time.deltaTime;
                    _uiGamePanelModel.CurrentCounter.Value = _playerModel.OpenChestSeconds.Value /
                                                             _playerModel.CurrentOpenChestNeedSeconds.Value;
                }));
            
            AddStates(new State<PlayState>()
                .WithKey(PlayState.PickingUp)
                .WithOnEnter(() =>
                {
                    _playerModel.CurrentState.Value = PlayState.PickingUp;
                })
                .WithOnExit(null)
                .WithOnTick(null));


            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.Swim)
                .WithToState(PlayState.UsingFishFork)
                .WithWeight(1)
                .AddConditions(() => _fishForkModel.CurrentFishForkState.Value != FishForkState.Ready));
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.Swim)
                .WithToState(PlayState.UsingGun)
                .WithWeight(1)
                .AddConditions(() => _gunModel.CurrentGunState.Value == GunState.Shooting));

            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.Swim)
                .WithToState(PlayState.Attack)
                .WithWeight(1)
                .AddConditions(() =>
                    _playerModel.CurrentWeaponType.Value == EquipWeaponKey.MeleeWeapon &&
                    !_playerModel.IfAttacking.Value && Input.GetMouseButtonDown(0)));
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.Swim)
                .WithToState(PlayState.OpeningTreasureChests)
                .WithWeight(1)
                .AddConditions(() => _playerModel.IfCanOpenTreasureChests.Value && Input.GetKeyDown(KeyCode.E)));
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.Swim)
                .WithToState(PlayState.PickingUp)
                .WithWeight(1)
                .AddConditions(() => _playerModel.IfCanPickUp.Value && Input.GetKeyDown(KeyCode.E)));
            
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.UsingFishFork)
                .WithToState(PlayState.CatchFish)
                .WithWeight(1)
                .AddConditions(() => _playerModel.IfCatchingFish.Value));
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.UsingFishFork)
                .WithToState(PlayState.Swim)
                .WithWeight(1)
                .AddConditions(() => _fishForkModel.CurrentFishForkState.Value == FishForkState.Ready));
            
            
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.UsingGun)
                .WithToState(PlayState.Swim)
                .WithWeight(1)
                .AddConditions(() =>
                {
                    return _gunModel.CurrentGunState.Value == GunState.Ready ||
                           _gunModel.CurrentGunState.Value == GunState.Cooling;
                }));
            
            
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.CatchFish)
                .WithToState(PlayState.Swim)
                .WithWeight(1)
                .AddConditions(() => !_playerModel.IfCatchingFish.Value));
            
            
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.Attack)
                .WithToState(PlayState.Swim)
                .WithWeight(1)
                .AddConditions(() => !_playerModel.IfAttacking.Value));
            
            
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.OpeningTreasureChests)
                .WithToState(PlayState.Swim)
                .WithWeight(1)
                .AddConditions(() => !_playerModel.IfCanOpenTreasureChests.Value || Input.GetKeyUp(KeyCode.E)));
            
            
            
            AddTransitions(new Transition<PlayState>()
                .WithFromState(PlayState.PickingUp)
                .WithToState(PlayState.Swim)
                .WithWeight(1)
                .AddConditions(() => !_playerModel.IfCanPickUp.Value));
            
            WithIdleState(PlayState.Swim);
        }

        private void swim()
        {
            var inputHorizontal = Input.GetAxis("Horizontal");
            var inputVertical = Input.GetAxis("Vertical");

            if (inputHorizontal < 0)
            {
                _player.transform.localScale = new Vector3(-1, 1, 0);
					
                _playerModel.IfLeft.Value = true;
            }
            else if (inputHorizontal > 0)
            {
                _player.transform.localScale = new Vector3(1, 1, 0);
					
                _playerModel.IfLeft.Value = false;
            }
				
            var direction = new Vector2(inputHorizontal, inputVertical).normalized;
            var playerTargetWalkingSpeed = direction * Config.PlayerWalkingRate;
            _player.mRigidbody2D.linearVelocity = Vector2.Lerp(_player.mRigidbody2D.linearVelocity,
                playerTargetWalkingSpeed, 1 - Mathf.Exp(-Time.deltaTime * 10));
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}