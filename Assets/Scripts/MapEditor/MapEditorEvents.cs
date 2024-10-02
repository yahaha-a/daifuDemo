using QFramework;
using UnityEngine;

namespace MapEditor
{
    public class MapEditorEvents
    {
        public static EasyEvent<MapEditorName, Vector3> CreateMapEditorItem = new EasyEvent<MapEditorName, Vector3>();
        
        public static EasyEvent<int> DeleteCreateItem = new EasyEvent<int>();

        public static EasyEvent<ICreateItemInfo> ShowCreateItem = new EasyEvent<ICreateItemInfo>();

        public static EasyEvent refreshCreatePanel = new EasyEvent();

    }
}