using System;
using UnityEngine;

namespace daifuDemo
{
    public class test : MonoBehaviour
    {
        private PlayerFsm playerFsm;

        private void Awake()
        {
            playerFsm = new PlayerFsm();
        }

        private void Update()
        {
            playerFsm.Tick();
        }
    }
}
