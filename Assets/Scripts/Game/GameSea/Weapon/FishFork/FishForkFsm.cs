using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum FishForkState
    {
        Ready,
        Aim,
        Revolve,
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
                .WithOnTick(null));
            
            AddStates(new State<FishForkState>()
                .WithKey(FishForkState.Revolve)
                .WithOnEnter(() => _fishForkModel.CurrentFishForkState.Value = FishForkState.Revolve)
                .WithOnExit(null)
                .WithOnTick(Revlove));
            
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
                .AddConditions(() => Input.GetKeyDown(KeyCode.I)));
            
            
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Aim)
                .WithToState(FishForkState.Revolve)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C)));
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Aim)
                .WithToState(FishForkState.Ready)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyUp(KeyCode.I)));
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Aim)
                .WithToState(FishForkState.Launch)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.J)));
            
            
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Revolve)
                .WithToState(FishForkState.Launch)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.J)));
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Revolve)
                .WithToState(FishForkState.Aim)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C)));
            
            AddTransitions(new Transition<FishForkState>()
                .WithFromState(FishForkState.Revolve)
                .WithToState(FishForkState.Ready)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyUp(KeyCode.I)));
            
            
            
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

        private void Revlove()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (_fishFork.transform.eulerAngles.z < 70f || _fishFork.transform.eulerAngles.z > 289f)
                {
                    var rotationAmount = _fishFork.rotationRate * Time.deltaTime;
                    _fishFork.transform.Rotate(new Vector3(0, 0, rotationAmount));
                }
            }
            else if (Input.GetKey(KeyCode.C))
            {
                if (_fishFork.transform.eulerAngles.z < 71f || _fishFork.transform.eulerAngles.z > 290f)
                {
                    var rotationAmount = -_fishFork.rotationRate * Time.deltaTime;
                    _fishFork.transform.Rotate(new Vector3(0, 0, rotationAmount));
                }
            }
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