using System;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
    public class GameGlobalController : MonoBehaviour, IController
    {
        private UIGameGlobalPanel _uiGameGlobalPanel;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Initialize()
        {
            GameObject gameGlobalController = new GameObject("GameGlobalController");
            gameGlobalController.AddComponent<GameGlobalController>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            this.SendCommand<InitializeDataCommand>();
            
            UIKit.Root.SetResolution(1920, 1080, 1);

            UIKit.OpenPanel<UIGameGlobalPanel>();
            _uiGameGlobalPanel = UIKit.GetPanel<UIGameGlobalPanel>();
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name != "MapEditor" && SceneManager.GetActiveScene().name != "GameStart")
            {
                if (!_uiGameGlobalPanel.gameObject.activeSelf)
                {
                    _uiGameGlobalPanel.Show();
                }
            }
            else
            {
                if (_uiGameGlobalPanel.gameObject.activeSelf)
                {
                    _uiGameGlobalPanel.Hide();
                }
            }
        }

        private void OnDestroy()
        {
            this.SendCommand<SaveDataCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}