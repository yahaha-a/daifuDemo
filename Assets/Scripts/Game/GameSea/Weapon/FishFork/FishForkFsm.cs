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
        private Transform _fishForkHeadTemplate;
        
        private GameObject _flyerRoot;
        
        private IFishForkModel _fishForkModel;
        private IFishForkHeadModel _fishForkHeadModel;

        public FishForkFsm(FishFork fishFork, Transform fishForkHeadTemplate)
        {
            _fishFork = fishFork;
            _fishForkHeadTemplate = fishForkHeadTemplate;
        }
        
        public override void Init()
        {
            _flyerRoot = GameObject.FindGameObjectWithTag("FlyerRoot");
            
            _fishForkModel = this.GetModel<IFishForkModel>();
            _fishForkHeadModel = this.GetModel<IFishForkHeadModel>();

            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Ready)
                .WithOnEnter(() => _fishForkModel.CurrentFishForkState.Value = FishForkState.Ready)
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Aim)
                .WithOnEnter(() => _fishForkModel.CurrentFishForkState.Value = FishForkState.Aim)
                .WithOnExit(null)
                .WithOnTick(() =>
                {
                    RotateTowardsMouse();
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

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _fishFork.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void Launch()
        {
            _fishForkHeadTemplate.InstantiateWithParent(_fishFork)
                .Self(self =>
                {
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