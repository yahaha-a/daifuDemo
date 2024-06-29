using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum GunState
    {
        Ready,
        Aim,
        Revolve,
        Shooting,
        Cooling
    }
    
    public class GunFsm : AbstractFsm<GunState>, IController
    {
        private Gun _gun;
        private Transform _bulletTemplate;
        
        private IGunModel _gunModel;

        public GunFsm(Gun gun, Transform bulletTemplate)
        {
            _gun = gun;
            _bulletTemplate = bulletTemplate;
        }
        
        public override void Init()
        {
            _gunModel = this.GetModel<IGunModel>();
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Ready)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Ready)
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Aim)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Aim)
                .WithOnExit(null)
                .WithOnTick(null));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Revolve)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Revolve)
                .WithOnExit(null)
                .WithOnTick(Revolve));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Shooting)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Shooting)
                .WithOnExit(null)
                .WithOnTick(Shooting));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Cooling)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Cooling)
                .WithOnExit(() => _gun.currentIntervalBetweenShots = 0f)
                .WithOnTick(() => _gun.currentIntervalBetweenShots += Time.deltaTime));



            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Ready)
                .WithToState(GunState.Aim)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.I)));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Aim)
                .WithToState(GunState.Revolve)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Aim)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyUp(KeyCode.I)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Aim)
                .WithToState(GunState.Shooting)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.J)));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Revolve)
                .WithToState(GunState.Shooting)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.J)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Revolve)
                .WithToState(GunState.Aim)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Revolve)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyUp(KeyCode.I)));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Shooting)
                .WithToState(GunState.Cooling)
                .WithWeight(1)
                .AddConditions(() => true));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Cooling)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => _gun.currentIntervalBetweenShots >= _gun.intervalBetweenShots));
        }

        private void Revolve()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                if (_gun.transform.eulerAngles.z < 70f || _gun.transform.eulerAngles.z > 289f)
                {
                    var rotationAmount = _gun.rotationRate * Time.deltaTime;
                    _gun.transform.Rotate(new Vector3(0, 0, rotationAmount));
                }
            }
            else if (Input.GetKey(KeyCode.C))
            {
                if (_gun.transform.eulerAngles.z < 71f || _gun.transform.eulerAngles.z > 290f)
                {
                    var rotationAmount = -_gun.rotationRate * Time.deltaTime;
                    _gun.transform.Rotate(new Vector3(0, 0, rotationAmount));
                }
            }
        }

        private void Shooting()
        {
            foreach (var (offsetDistance, launchDirection) in _gun.BulletSpawnLocationsAndDirectionsList)
            {
                _bulletTemplate.InstantiateWithParent(_gun)
                    .Self(self =>
                    {
                        self.position = new Vector3(self.position.x + offsetDistance.x,
                            self.position.y + offsetDistance.y, self.position.z);
                        self.Rotate(new Vector3(0, 0, launchDirection));
								
                        if (_gun.ifLeft)
                        {
                            self.GetComponent<Bullet>().Direction = -1;
                        }
                        else
                        {
                            self.GetComponent<Bullet>().Direction = 1;
                        }
                        self.parent = _gun.flyerRoot.transform;
                        self.Show();
                    });
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}