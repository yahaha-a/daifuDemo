using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum GunState
    {
        Ready,
        Aim,
        Shooting,
        Cooling,
        LoadAmmunition
    }
    
    public class GunFsm : AbstractFsm<GunState>, IController
    {
        private Gun _gun;
        
        private Transform _bulletTemplate;
        
        private IGunModel _gunModel;

        private IBulletModel _bulletModel;

        private IBulletSystem _bulletSystem;

        public GunFsm(Gun gun, Transform bulletTemplate)
        {
            _gun = gun;
            _bulletTemplate = bulletTemplate;
        }
        
        public override void Init()
        {
            _gunModel = this.GetModel<IGunModel>();

            _bulletModel = this.GetModel<IBulletModel>();

            _bulletSystem = this.GetSystem<IBulletSystem>();
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Ready)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Ready)
                .WithOnExit(null)
                .WithOnTick(RotateTowardsMouse));

            AddStates(new State<GunState>()
                .WithKey(GunState.Aim)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Aim)
                .WithOnExit(null)
                .WithOnTick(() =>
                {
                    RotateTowardsMouse();
                }));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Shooting)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Shooting)
                .WithOnExit(null)
                .WithOnTick(Shooting));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Cooling)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Cooling)
                .WithOnExit(() => _gun.currentIntervalBetweenShots = _gun.intervalBetweenShots)
                .WithOnTick(() =>
                {
                    _gun.currentIntervalBetweenShots -= Time.deltaTime;
                    RotateTowardsMouse();
                }));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.LoadAmmunition)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.LoadAmmunition)
                .WithOnExit(() =>
                {
                    _gun.currentLoadAmmunitionTime = _gun.loadAmmunitionNeedTime;
                    _gun.ifMeetReloadAmmunitionTime = false;
                })
                .WithOnTick(() =>
                {
                    _gun.currentLoadAmmunitionTime -= Time.deltaTime;
                    RotateTowardsMouse();

                    if (_gun.currentLoadAmmunitionTime <= 0)
                    {
                        _gun.ifMeetReloadAmmunitionTime = true;
                        LoadAmmunition();
                    }
                }));

            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Ready)
                .WithToState(GunState.Shooting)
                .WithWeight(3)
                .AddConditions(() => Input.GetMouseButtonDown(0)));

            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Ready)
                .WithToState(GunState.Aim)
                .WithWeight(2)
                .AddConditions(() => Input.GetMouseButtonDown(1)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Ready)
                .WithToState(GunState.LoadAmmunition)
                .WithWeight(1)
                .AddConditions(() => _gun.currentAmmunition.Value != _gun.maximumAmmunition && Input.GetKeyDown(KeyCode.R)));

            

            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Aim)
                .WithToState(GunState.Shooting)
                .WithWeight(3)
                .AddConditions(() => Input.GetMouseButtonDown(0)));

            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Aim)
                .WithToState(GunState.LoadAmmunition)
                .WithWeight(2)
                .AddConditions(() => Input.GetKeyDown(KeyCode.R)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Aim)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => Input.GetMouseButtonUp(1)));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Shooting)
                .WithToState(GunState.Cooling)
                .WithWeight(1)
                .AddConditions(() => true));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Cooling)
                .WithToState(GunState.LoadAmmunition)
                .WithWeight(2)
                .AddConditions(() => Input.GetKeyDown(KeyCode.R)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Cooling)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => _gun.currentIntervalBetweenShots <= 0));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.LoadAmmunition)
                .WithToState(GunState.Shooting)
                .WithWeight(3)
                .AddConditions(() => Input.GetMouseButtonDown(0)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.LoadAmmunition)
                .WithToState(GunState.Aim)
                .WithWeight(2)
                .AddConditions(() => Input.GetMouseButtonDown(1)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.LoadAmmunition)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => _gun.ifMeetReloadAmmunitionTime));
        }

        private void RotateTowardsMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 gunPosition = _gun.transform.position;
            Vector3 direction = mousePosition - gunPosition;
    
            if (_gunModel.IfLeft.Value)
            {
                direction.x = -direction.x;
                direction.y = -direction.y;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -60f, 60f);

            _gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void Shooting()
        {
            if (_gun.currentAmmunition.Value >= _gun.bulletSpawnLocationsAndDirectionsList.Count)
            {
                foreach (var (offsetDistance, launchDirection) in _gun.bulletSpawnLocationsAndDirectionsList)
                {
                    _bulletTemplate.InstantiateWithParent(_gun)
                        .Self(self =>
                        {
                            Bullet bullet = self.GetComponent<Bullet>();
                            bullet.damage = _bulletSystem.BulletInfos[_gun.key]
                                .Find(item => item.Type == _bulletModel.CurrentBulletType.Value).Damage;
                            bullet.speed = _gun.rateOfFire;
                            bullet.range = _gun.attackRange;
                            if (_gunModel.IfLeft.Value)
                            {
                                bullet.direction = -1;
                            }
                            else
                            {
                                bullet.direction = 1;
                            }
                            
                            self.position = new Vector3(self.position.x + offsetDistance.x,
                                self.position.y + offsetDistance.y, self.position.z);
                            self.Rotate(new Vector3(0, 0, launchDirection));
                            self.parent = _gun.flyerRoot.transform;
                            
                            self.Show();
                        });
                    _gun.currentAmmunition.Value--;
                }
            }
        }

        private void LoadAmmunition()
        {
            var needSupplement = _gun.maximumAmmunition - _gun.currentAmmunition.Value;

            if (_gun.currentAllAmmunition.Value >= needSupplement)
            {
                _gun.currentAllAmmunition.Value -= needSupplement;
                _gun.currentAmmunition.Value = _gun.maximumAmmunition;
            }
            else
            {
                _gun.currentAmmunition.Value += _gun.currentAllAmmunition.Value;
                _gun.currentAllAmmunition.Value = 0;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}