using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace daifuDemo
{
    public class GameGlobalController : MonoBehaviour, IController
    {
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