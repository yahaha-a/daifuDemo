using System;
using UnityEngine;

namespace MapEditor
{
    public class CameraController : MonoBehaviour
    {
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontal, vertical, 0);

            transform.position += moveDirection * 5 * Time.deltaTime;
        }
    }
}