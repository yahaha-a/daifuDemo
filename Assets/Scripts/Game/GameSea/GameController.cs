using System;
using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace daifuDemo
{
	public partial class GameController : ViewController, IController
	{
		private static ResLoader _resLoader = ResLoader.Allocate();
		
		private IPlayerModel _playerModel;

		private IUIGameShipPanelModel _uiGameShipPanelModel;

		private IMapCreateSystem _mapCreateSystem;

		private IWeaponSystem _weaponSystem;
		
		private Transform BarrierRoot;
		private Transform PlayerRoot;
		private Transform FishRoot;
		private Transform TreasureChestRoot;
		private Transform DestructibleRoot;
		private Transform DropsRoot;
		private Transform Player;

		private GameObject BarrierPrefab;
		private GameObject RolePrefab;
		private GameObject NormalFishPrefab;
		private GameObject AggressiveFishPrefab;
		private GameObject TreasureChestPrefab;
		private GameObject DestructibleItemPrefab;
		private GameObject DropItemPrefab;

		private GameObject FishForkPrefab;
		private GameObject MeleeWeaponPrefab;
		private GameObject GunPrefab;
		
		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();
			_uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
			_mapCreateSystem = this.GetSystem<IMapCreateSystem>();
			_weaponSystem = this.GetSystem<IWeaponSystem>();
			
			BarrierRoot = GameObject.FindGameObjectWithTag("BarrierRoot").transform;
			PlayerRoot = GameObject.FindGameObjectWithTag("PlayerRoot").transform;
			FishRoot = GameObject.FindGameObjectWithTag("FishRoot").transform;
			TreasureChestRoot = GameObject.FindGameObjectWithTag("TreasureChestRoot").transform;
			DestructibleRoot = GameObject.FindGameObjectWithTag("DestructibleRoot").transform;
			DropsRoot = GameObject.FindGameObjectWithTag("DropsRoot").transform;

			BarrierPrefab = _resLoader.LoadSync<GameObject>("Clod");
			RolePrefab = _resLoader.LoadSync<GameObject>("Dave");
			NormalFishPrefab = _resLoader.LoadSync<GameObject>("NormalFish");
			AggressiveFishPrefab = _resLoader.LoadSync<GameObject>("AggressiveFish");
			TreasureChestPrefab = _resLoader.LoadSync<GameObject>("TreasureChest");
			DestructibleItemPrefab = _resLoader.LoadSync<GameObject>("DestructibleItem");
			DropItemPrefab = _resLoader.LoadSync<GameObject>("DropItem");

			FishForkPrefab = _resLoader.LoadSync<GameObject>("FishFork");
			MeleeWeaponPrefab = _resLoader.LoadSync<GameObject>("MeleeWeapon");
			GunPrefab = _resLoader.LoadSync<GameObject>("Gun");

			_playerModel.PlayerOxygen.Register(oxygen =>
			{
				if (oxygen <= 0)
				{
					GameObject.FindGameObjectWithTag("Player").DestroySelf();
					UIKit.OpenPanel<UIGameOverPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			CreateMap();
			Events.MapInitializationComplete?.Trigger();
			
			UIKit.OpenPanel<UIGamePanel>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.B))
			{
				this.SendCommand<OpenOrCloseBackpackCommand>();
			}
		}

		public void CreateMap()
		{
			foreach (var item in _mapCreateSystem.GetCreateItemInfos())
			{
				if (item.Type == CreateItemType.Barrier)
				{
					BarrierPrefab.InstantiateWithParent(BarrierRoot).Self(self =>
					{
						self.transform.position = item.Position;
					});
				}
				else if (item.Type == CreateItemType.Role)
				{
					RolePrefab.InstantiateWithParent(PlayerRoot).Self(self =>
					{
						self.transform.position = item.Position;
					});

					Player = GameObject.FindGameObjectWithTag("Player").transform;
					CreateWeapon();
				}
				else if (item.Type == CreateItemType.Fish)
				{
					for (int i = 0; i < item.Number; i++)
					{
						if (item.Key == "NormalFish")
						{
							NormalFishPrefab.InstantiateWithParent(FishRoot).Self(self =>
							{
								self.GetComponent<NormalFish>().StartPosition = item.Position;
								self.transform.position = GetRandomPositionInCircle(item.Range / 100, item.Position);
								self.GetComponent<NormalFish>().RangeOfMovement = item.Range / 200;
							});
						}
						else if (item.Key == "AggressiveFish")
						{
							AggressiveFishPrefab.InstantiateWithParent(FishRoot).Self(self =>
							{
								self.GetComponent<Pterois>().StartPosition = item.Position;
								self.transform.position = GetRandomPositionInCircle(item.Range / 100, item.Position);
								self.GetComponent<Pterois>().RangeOfMovement = item.Range / 200;
							});
						}
					}
				}
				else if (item.Type == CreateItemType.TreasureChests)
				{
					TreasureChestPrefab.InstantiateWithParent(TreasureChestRoot).Self(self =>
					{
						self.GetComponent<TreasureBox>().key = item.Key;
						self.transform.position = item.Position;
					});
				}
				else if (item.Type == CreateItemType.Destructible)
				{
					DestructibleItemPrefab.InstantiateWithParent(DestructibleRoot).Self(self =>
					{
						self.GetComponent<StrikeItem>().key = item.Key;
						self.transform.position = item.Position;
					});
				}
				else if (item.Type == CreateItemType.Drops)
				{
					DropItemPrefab.InstantiateWithParent(DropsRoot).Self(self =>
					{
						self.GetComponent<PickUpItem>().key = item.Key;
						self.transform.position = item.Position;
					});
				}
			}
			
			Events.LoadMapComplete?.Trigger();
		}

		public void CreateWeapon()
		{
			if (_uiGameShipPanelModel.CurrentEquipFishFork.Value != null)
			{
				FishForkPrefab.InstantiateWithParent(Player).Self(self =>
				{
					var fishFork = self.GetComponent<FishFork>();
					fishFork.key = _uiGameShipPanelModel.CurrentEquipFishFork.Value.Key;
					fishFork.currentRank = _uiGameShipPanelModel.CurrentEquipFishFork.Value.Rank;
					fishFork.weaponName = _uiGameShipPanelModel.CurrentEquipFishFork.Value.Name;
					_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.FishFork, self);
				});
			}
			else
			{
				_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.FishFork, null);
			}

			if (_uiGameShipPanelModel.CurrentEquipMeleeWeapon.Value != null)
			{
				MeleeWeaponPrefab.InstantiateWithParent(Player).Self(self =>
				{
					var meleeWeapon = self.GetComponent<MeleeWeapon>();
					meleeWeapon.key = _uiGameShipPanelModel.CurrentEquipMeleeWeapon.Value.Key;
					meleeWeapon.currentRank = _uiGameShipPanelModel.CurrentEquipMeleeWeapon.Value.Rank;
					meleeWeapon.weaponName = _uiGameShipPanelModel.CurrentEquipMeleeWeapon.Value.Name;
					_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.MeleeWeapon, self);
				});
			}
			else
			{
				_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.MeleeWeapon, null);
			}

			if (_uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value != null)
			{
				GunPrefab.InstantiateWithParent(Player).Self(self =>
				{
					var gun = self.GetComponent<Gun>();
					gun.key = _uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value.Key;
					gun.currentRank = _uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value.Rank;
					gun.weaponName = _uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value.Name;
					gun.currentAllAmmunition.Value = 
						_uiGameShipPanelModel.CurrentEquipPrimaryWeapon.Value.AmmunitionNumber.Value;
					_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.PrimaryWeapon, self);
				});
			}
			else
			{
				_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.PrimaryWeapon, null);
			}

			if (_uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value != null)
			{
				GunPrefab.InstantiateWithParent(Player).Self(self =>
				{
					var gun = self.GetComponent<Gun>();
					gun.key = _uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value.Key;
					gun.currentRank = _uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value.Rank;
					gun.currentAllAmmunition.Value =
						_uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value.AmmunitionNumber.Value;
					gun.weaponName = _uiGameShipPanelModel.CurrentEquipSecondaryWeapons.Value.Name;
					_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.SecondaryWeapons, self);
				});
			}
			else
			{
				_weaponSystem.CurrentEquipWeapons.Add(EquipWeaponKey.SecondaryWeapons, null);
			}
		}
		
		public Vector2 GetRandomPositionInCircle(float range, Vector3 center)
		{
			float radius = range / 2f;
			Vector2 randomPosition;

			float angle = Random.Range(0f, 2f * Mathf.PI);
			float r = Random.Range(0f, radius);

			randomPosition = new Vector2(
				center.x + r * Mathf.Cos(angle),
				center.y + r * Mathf.Sin(angle)
			);

			return randomPosition;
		}
		
		private void ClearChildren(Transform parent)
		{
			foreach (Transform child in parent)
			{
				GameObject.Destroy(child.gameObject);
			}
		}

		private void ClearAllRoots()
		{
			if (BarrierRoot != null)
			{
				ClearChildren(BarrierRoot);
			}
    
			if (PlayerRoot != null)
			{
				ClearChildren(PlayerRoot);
			}
    
			if (FishRoot != null)
			{
				ClearChildren(FishRoot);
			}
    
			if (TreasureChestRoot != null)
			{
				ClearChildren(TreasureChestRoot);
			}
    
			if (DestructibleRoot != null)
			{
				ClearChildren(DestructibleRoot);
			}
    
			if (DropsRoot != null)
			{
				ClearChildren(DropsRoot);
			}
		}


		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
