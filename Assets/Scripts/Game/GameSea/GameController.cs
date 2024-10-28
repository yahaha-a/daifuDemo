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

		private IMapCreateSystem _mapCreateSystem;
		
		private Transform BarrierRoot;
		private Transform PlayerRoot;
		private Transform FishRoot;
		private Transform TreasureChestRoot;
		private Transform DestructibleRoot;
		private Transform DropsRoot;

		private GameObject BarrierPrefab;
		private GameObject RolePrefab;
		private GameObject NormalFishPrefab;
		private GameObject AggressiveFishPrefab;
		private GameObject TreasureChestPrefab;
		private GameObject DestructibleItemPrefab;
		private GameObject DropItemPrefab;
		
		private void Start()
		{
			_playerModel = this.GetModel<IPlayerModel>();
			_mapCreateSystem = this.GetSystem<IMapCreateSystem>();
			
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
