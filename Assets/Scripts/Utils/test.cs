using System;
using UnityEngine;

namespace daifuDemo
{
    public class test : MonoBehaviour
    {
        private PlayerFsm _playerFsm;
        private PlayerBehaviorTree _playerBehaviorTree;

        private void Awake()
        {
            _playerFsm = new PlayerFsm();
            _playerBehaviorTree = new PlayerBehaviorTree();
        }

        private void Update()
        {
            _playerFsm.Tick();
            _playerBehaviorTree.Tick();
        }
    }
}
