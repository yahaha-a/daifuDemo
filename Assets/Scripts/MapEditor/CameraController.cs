using System;
using QFramework;
using UnityEngine;

namespace MapEditor
{
    public class CameraController : MonoBehaviour, IController
    {
        private IMapEditorModel _mapEditorModel;
        private IMapEditorSystem _mapEditorSystem;
        
        private Vector3 _dragOrigin;
        public float zoomSpeed = 2.0f;
        public float minZoom = 5.0f;
        public float maxZoom = 10.0f;
        
        private void Start()
        {
            _mapEditorModel = this.GetModel<IMapEditorModel>();
            _mapEditorSystem = this.GetSystem<IMapEditorSystem>();
            
            MapEditorEvents.LoadArchive.Register(() =>
            {
                transform.position = new Vector3(0, 0, -10);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(2))
            {
                Vector3 difference = _dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position += difference;
            }

            if (_mapEditorSystem._mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].OptionType !=
                OptionType.Range)
            {
                float scroll = Input.GetAxis("Mouse ScrollWheel");
                if (scroll != 0)
                {
                    Camera.main.orthographicSize -= scroll * zoomSpeed;
                    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
                }
            }
        }

        public IArchitecture GetArchitecture()
        {
            return MapEditorGlobal.Interface;
        }
    }
}