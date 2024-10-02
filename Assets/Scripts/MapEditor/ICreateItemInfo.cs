using UnityEngine;

namespace MapEditor
{
    public enum CreateItemType
    {
        Null,
        Fish,
        TreasureChests,
        Destructible
    }
    
    public interface ICreateItemInfo
    {
        MapEditorName Key { get; }
        
        int SerialNumber { get; }
        
        float X { get; }
        
        float Y { get; }
        
        float Range { get; }

        ICreateItemInfo WithKey(MapEditorName key);

        ICreateItemInfo WithSerialNumber(int serialNumber);

        ICreateItemInfo WithX(float x);

        ICreateItemInfo WithY(float y);

        ICreateItemInfo WithRange(float range);
    }

    public class CreateItemInfo : ICreateItemInfo
    {
        public MapEditorName Key { get; set; }
        
        public int SerialNumber { get; set; }

        public float X { get; set; }
        
        public float Y { get; set; }
        
        public float Range { get; set; }

        public ICreateItemInfo WithKey(MapEditorName key)
        {
            Key = key;
            return this;
        }

        public ICreateItemInfo WithSerialNumber(int serialNumber)
        {
            SerialNumber = serialNumber;
            return this;
        }

        public ICreateItemInfo WithX(float x)
        {
            X = x;
            return this;
        }

        public ICreateItemInfo WithY(float y)
        {
            Y = y;
            return this;
        }

        public ICreateItemInfo WithRange(float range)
        {
            Range = range;
            return this;
        }
    }
}