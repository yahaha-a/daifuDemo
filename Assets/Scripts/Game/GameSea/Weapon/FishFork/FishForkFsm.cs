using System;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum FishForkState
    {
        Ready,
        Aim,
        Launch,
        Shooting
    }
    
    public class FishForkFsm : AbstractFsm<FishForkState>, IController
    {
        private FishFork _fishFork;
        
        private GameObject _flyerRoot;
        private Transform _playerTransform;
        private AttackRangeIndicator _attackRangeIndicator;

        private IFishForkModel _fishForkModel;
        private IFishForkHeadModel _fishForkHeadModel;
        private IIndicatorSystem _indicatorSystem;

        public FishForkFsm(FishFork fishFork)
        {
            _fishFork = fishFork;
        }
        
        public override void Init()
        {
            _flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");

            _fishForkModel = this.GetModel<IFishForkModel>();
            _fishForkHeadModel = this.GetModel<IFishForkHeadModel>();
            _indicatorSystem = this.GetSystem<IIndicatorSystem>();

            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _attackRangeIndicator = _indicatorSystem.CreateIndicator(_playerTransform);

            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Ready)
                .WithOnEnter(() =>
                {
                    _fishForkModel.CurrentFishForkState.Value = FishForkState.Ready;
                    _fishFork.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                })
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Aim)
                .WithOnEnter(() =>
                {
                    _fishForkModel.CurrentFishForkState.Value = FishForkState.Aim;
                    _attackRangeIndicator.Show();
                })
                .WithOnExit(() =>
                {
                    _fishFork.currentChargingTime = 0;
                    _attackRangeIndicator.Hide();
                })
                .WithOnTick(() =>
                {
                    RotateTowardsMouse();
                    Charge();
                    _indicatorSystem.CreateSectorMesh(_attackRangeIndicator.mesh, Vector3.zero,
                        _fishFork.currentFishForkLength + 1f, 1f, 120, 60);
                    if (_fishFork.ifLeft)
                    {
                        _indicatorSystem.CreateLine(_attackRangeIndicator.lineRender, _fishFork.currentFishForkLength, 0.2f, -_fishFork.transform.right, _fishFork.FishForkHeadTemplate.position);
                    }
                    else
                    {
                        _indicatorSystem.CreateLine(_attackRangeIndicator.lineRender, _fishFork.currentFishForkLength, 0.2f, _fishFork.transform.right, _fishFork.FishForkHeadTemplate.position);
                    }
                }));
            
            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Launch)
                .WithOnEnter(() =>
                {
                    _fishForkModel.CurrentFishForkState.Value = FishForkState.Launch;
                    Launch();
                })
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Shooting)
                .WithOnEnter(() => _fishForkModel.CurrentFishForkState.Value = FishForkState.Shooting)
                .WithOnExit(null)
                .WithOnTick(null));



            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Ready)
                .WithToState(FishForkState.Aim)
                .WithWeight(1)
                .AddConditions(() => Input.GetMouseButtonDown(1)));
            
            
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Aim)
                .WithToState(FishForkState.Ready)
                .WithWeight(1)
                .AddConditions(() => Input.GetMouseButtonUp(1)));
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Aim)
                .WithToState(FishForkState.Launch)
                .WithWeight(1)
                .AddConditions(() => Input.GetMouseButtonDown(0)));
            
            
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Launch)
                .WithToState(FishForkState.Shooting)
                .WithWeight(1)
                .AddConditions(() => true));
            
            
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Shooting)
                .WithToState(FishForkState.Ready)
                .WithWeight(1)
                .AddConditions(() => !_fishForkModel.IfFishForkHeadExist.Value));

            WithIdleState(FishForkState.Ready);
        }

        private void RotateTowardsMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 fishForkPosition = _fishFork.transform.position;
            Vector3 direction = mousePosition - fishForkPosition;
    
            if (_fishFork.ifLeft)
            {
                direction.x = -direction.x;
                direction.y = -direction.y;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -60f, 60f);

            _fishFork.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void Charge()
        {
            _fishFork.currentChargingTime += Time.deltaTime;
            _fishFork.currentFishForkLength = Math.Clamp(_fishFork.currentChargingTime / _fishFork.chargingTime, 0, 1) *
                                              _fishFork.fishForkLength;
        }
        
        private void Launch()
        {
            _fishFork.FishForkHeadTemplate.InstantiateWithParent(_fishFork)
                .Self(self =>
                {
                    var fishForkHead = self.GetComponent<FishForkHead>();
                    fishForkHead.speed = _fishFork.launchSpeed;
                    fishForkHead.fishForkLength = _fishFork.currentFishForkLength;
                    if (_fishFork.ifLeft)
                    {
                        _fishForkHeadModel.FishForkHeadDirection = -1;
                    }
                    else
                    {
                        _fishForkHeadModel.FishForkHeadDirection = 1;
                    }
                    self.parent = _flyerRoot.transform;
                    self.Show();
                });

            _fishForkModel.IfFishForkHeadExist.Value = true;
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}