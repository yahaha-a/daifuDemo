using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum GunState
    {
        Ready,
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

        private ISingleUseItemsModel _singleUseItemsModel;

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

            _singleUseItemsModel = this.GetModel<ISingleUseItemsModel>();

            _bulletSystem = this.GetSystem<IBulletSystem>();
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Ready)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Ready)
                .WithOnExit(null)
                .WithOnTick(RotateTowardsMouse));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Shooting)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Shooting)
                .WithOnExit(null)
                .WithOnTick(Shooting));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.Cooling)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.Cooling)
                .WithOnExit(() => _gunModel.CurrentIntervalBetweenShots.Value = _gunModel.IntervalBetweenShots.Value)
                .WithOnTick(() =>
                {
                    _gunModel.CurrentIntervalBetweenShots.Value -= Time.deltaTime;
                    RotateTowardsMouse();
                }));
            
            AddStates(new State<GunState>()
                .WithKey(GunState.LoadAmmunition)
                .WithOnEnter(() => _gunModel.CurrentGunState.Value = GunState.LoadAmmunition)
                .WithOnExit(() =>
                {
                    _gunModel.CurrentLoadAmmunitionTime.Value = _gunModel.LoadAmmunitionNeedTime.Value;
                    LoadAmmunition();
                })
                .WithOnTick(() =>
                {
                    _gunModel.CurrentLoadAmmunitionTime.Value -= Time.deltaTime;
                    RotateTowardsMouse();
                }));

            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Ready)
                .WithToState(GunState.Shooting)
                .WithWeight(2)
                .AddConditions(() => Input.GetMouseButtonDown(0)));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Ready)
                .WithToState(GunState.LoadAmmunition)
                .WithWeight(1)
                .AddConditions(() => Input.GetKeyDown(KeyCode.R)));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Shooting)
                .WithToState(GunState.Cooling)
                .WithWeight(1)
                .AddConditions(() => true));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Cooling)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => _gunModel.CurrentIntervalBetweenShots.Value <= 0));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.Cooling)
                .WithToState(GunState.LoadAmmunition)
                .WithWeight(2)
                .AddConditions(() => Input.GetKeyDown(KeyCode.R)));
            
            
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.LoadAmmunition)
                .WithToState(GunState.Ready)
                .WithWeight(1)
                .AddConditions(() => _gunModel.CurrentLoadAmmunitionTime.Value <= 0));
            
            AddTransitions(new Transition<GunState>()
                .WithFromState(GunState.LoadAmmunition)
                .WithToState(GunState.Shooting)
                .WithWeight(2)
                .AddConditions(() => Input.GetMouseButtonDown(0)));
        }

        private void RotateTowardsMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
            mousePosition.z = 0f;

            Vector3 fishForkPosition = _gun.transform.position;

            Vector3 direction = mousePosition - fishForkPosition;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void Shooting()
        {
            if (_gunModel.CurrentAmmunition.Value >= _gunModel.BulletSpawnLocationsAndDirectionsList.Value.Count)
            {
                foreach (var (offsetDistance, launchDirection) in _gunModel.BulletSpawnLocationsAndDirectionsList.Value)
                {
                    _bulletTemplate.InstantiateWithParent(_gun)
                        .Self(self =>
                        {
                            Bullet bullet = self.GetComponent<Bullet>();
                            bullet.damage = _bulletSystem.BulletInfos[_gunModel.CurrentGunKey.Value]
                                .Find(item => item.Type == _bulletModel.CurrentBulletType.Value).Damage;
                            bullet.speed = _gunModel.RateOfFire.Value;
                            bullet.range = _gunModel.AttackRange.Value;
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
                    _gunModel.CurrentAmmunition.Value--;
                }
            }
        }

        private void LoadAmmunition()
        {
            if (_gunModel.CurrentGunKey.Value == Config.RifleKey)
            {
                var needSupplement = _gunModel.MaximumAmmunition.Value - _gunModel.CurrentAmmunition.Value;

                if (_singleUseItemsModel.RifleBulletCount.Value >= needSupplement)
                {
                    _singleUseItemsModel.RifleBulletCount.Value -= needSupplement;
                    _gunModel.CurrentAmmunition.Value = _gunModel.MaximumAmmunition.Value;
                }
                else
                {
                    _gunModel.CurrentAmmunition.Value += _singleUseItemsModel.RifleBulletCount.Value;
                    _singleUseItemsModel.RifleBulletCount.Value = 0;
                }
            }
            else if (_gunModel.CurrentGunKey.Value == Config.ShotgunKey)
            {
                var needSupplement = _gunModel.MaximumAmmunition.Value - _gunModel.CurrentAmmunition.Value;

                if (_singleUseItemsModel.ShotgunBulletCount.Value >= needSupplement)
                {
                    _singleUseItemsModel.ShotgunBulletCount.Value -= needSupplement;
                    _gunModel.CurrentAmmunition.Value = _gunModel.MaximumAmmunition.Value;
                }
                else
                {
                    _gunModel.CurrentAmmunition.Value += _singleUseItemsModel.ShotgunBulletCount.Value;
                    _singleUseItemsModel.ShotgunBulletCount.Value = 0;
                }
            }
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}