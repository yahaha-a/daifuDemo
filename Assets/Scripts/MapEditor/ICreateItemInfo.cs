using Global;
using UnityEngine;

namespace MapEditor
{
    public enum CreateItemType
    {
        Null,
        Barrier,
        Role,
        Fish,
        TreasureChests,
        Destructible,
        Drops
    }
    
    public interface ICreateItemInfo
    {
        CreateItemName Key { get; }
        
        int SerialNumber { get; }
        
        float X { get; }
        
        float Y { get; }
        
        float Range { get; }
        
        int Number { get; }

        ICreateItemInfo WithKey(CreateItemName key);

        ICreateItemInfo WithSerialNumber(int serialNumber);

        ICreateItemInfo WithX(float x);

        ICreateItemInfo WithY(float y);

        ICreateItemInfo WithRange(float range);

        ICreateItemInfo WithNumber(int number);
    }

    public class CreateItemInfo : ICreateItemInfo
    {
        public CreateItemName Key { get; set; }
        
        public int SerialNumber { get; set; }

        public float X { get; set; }
        
        public float Y { get; set; }
        
        public float Range { get; set; }
        
        public int Number { get; set; }

        public ICreateItemInfo WithKey(CreateItemName key)
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

        public ICreateItemInfo WithNumber(int number)
        {
            Number = number;
            return this;
        }
    }
}