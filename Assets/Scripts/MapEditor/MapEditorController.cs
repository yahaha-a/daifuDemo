using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MapEditor
{
    public class MapEditorController : MonoBehaviour, IController
    {
        [SerializeField]
        public RectTransform SelectItemCursor;
        [SerializeField]
        public UnityEngine.UI.Image SelectSingleIcon;
        [SerializeField]
        public UnityEngine.UI.Image SelectRangeIcon;
        [SerializeField]
        public UnityEngine.UI.Text SelectItemCursorName;
        
        private IMapEditorModel _mapEditorModel;
        private IMapEditorSystem _mapEditorSystem;
        
        private MapEditorName _currentMapEditorName;
        
        private void Start()
        {
            UIKit.Root.SetResolution(1920, 1080, 1);
            UIKit.OpenPanel<UIMapEditor>();

            _mapEditorModel = this.GetModel<IMapEditorModel>();
            _mapEditorSystem = this.GetSystem<IMapEditorSystem>();

            _mapEditorModel.CurrentArchiveName.Register(name =>
            {
                _mapEditorSystem.LoadItemsFromXml(name);
                _mapEditorModel.CurrentSerialNumber.Value = _mapEditorSystem.GetMaxSerialNumberInXml(name) + 1;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            this.GetModel<IMapEditorModel>().CurrentMapEditorName.RegisterWithInitValue(name =>
            {
                _currentMapEditorName = name;
                SelectItemCursor.sizeDelta = new Vector2(100, 100);
                SelectSingleIcon.Hide();
                SelectRangeIcon.Hide();
                SelectItemCursorName.Hide();
                SelectItemCursorName.text = _mapEditorSystem._mapEditorInfos[_currentMapEditorName].Name;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            var worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            worldMousePosition.x = MathF.Floor(worldMousePosition.x + 0.5f);
            worldMousePosition.y = MathF.Floor(worldMousePosition.y + 0.5f);
            worldMousePosition.z = 0;

            SelectItemCursor.position = worldMousePosition;
            _mapEditorModel.CurrentMousePosition.Value = worldMousePosition;

            if (_mapEditorSystem._mapEditorInfos[_currentMapEditorName].OptionType == OptionType.Null)
            {
				
            }
            else if (_mapEditorSystem._mapEditorInfos[_currentMapEditorName].OptionType == OptionType.Single)
            {
                SelectSingleIcon.Show();
                SelectItemCursorName.Show();
            }
            else if (_mapEditorSystem._mapEditorInfos[_currentMapEditorName].OptionType == OptionType.Range)
            {
                var range = this.GetModel<IMapEditorModel>().CurrentSelectRange.Value;
                SelectItemCursor.sizeDelta = new Vector2(range, range);
				
                SelectRangeIcon.Show();
                SelectItemCursorName.Show();
            }
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0f)
            {
                this.GetModel<IMapEditorModel>().CurrentSelectRange.Value += 10f;
            }
            else if (scroll < 0f)
            {
                this.GetModel<IMapEditorModel>().CurrentSelectRange.Value -= 10f;
            }
            
            if (Input.GetMouseButtonDown(0) && IfCanCreate())
            {
                var x = _mapEditorModel.CurrentMousePosition.Value.x;
                var y = _mapEditorModel.CurrentMousePosition.Value.y;
                
                if (_mapEditorSystem._mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].OptionType ==
                    OptionType.Null)
                {
                    
                }
                else if (_mapEditorSystem._mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].OptionType ==
                         OptionType.Single)
                {
                    MapEditorEvents.CreateMapEditorItem?.Trigger(_mapEditorModel.CurrentMapEditorName.Value,
                        new Vector3(x, y, 100));
                }
                else if (_mapEditorSystem._mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].OptionType ==
                         OptionType.Range)
                {
                    MapEditorEvents.CreateMapEditorItem?.Trigger(_mapEditorModel.CurrentMapEditorName.Value,
                        new Vector3(x, y, _mapEditorModel.CurrentSelectRange.Value));
                }
            }
        }

        bool IfCanCreate()
        {
            if (_mapEditorModel.CurrentArchiveName.Value == null)
            {
                return false;
            }
            
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            if (raycastResults.Count == 0)
            {
                return true;
            }

            return false;
        }

        public IArchitecture GetArchitecture()
        {
            return MapEditorGlobal.Interface;
        }
    }
}

