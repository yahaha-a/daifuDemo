using Global;
using QFramework;
using UnityEngine;

namespace MapEditor
{
    public class MapEditorEvents
    {
        public static EasyEvent CreateMapEditorItem = new EasyEvent();
        
        public static EasyEvent<int> DeleteCreateItem = new EasyEvent<int>();

        public static EasyEvent<ICreateItemInfo> ShowCreateItem = new EasyEvent<ICreateItemInfo>();

        public static EasyEvent refreshCreatePanel = new EasyEvent();

        public static EasyEvent<float> ShowInputCreateNumber = new EasyEvent<float>();

        public static EasyEvent HideInputCreateNumber = new EasyEvent();

        public static EasyEvent LoadArchive = new EasyEvent();
    }
}