using UnityEngine;

namespace daifuDemo
{
    public interface IObtainItemsInfo
    {
        string Name { get; }
        
        string IconKey { get; }
        
        int Number { get; }
        
        Vector2 StartPosition { get; }

        IObtainItemsInfo WithName(string name);

        IObtainItemsInfo WithIconKey(string iconKey);

        IObtainItemsInfo WithNumber(int number);

        IObtainItemsInfo WithStartPosition(Vector2 position);
    }

    public class ObtainItemsInfo : IObtainItemsInfo
    {
        public string Name { get; set; }
        
        public string IconKey { get; set; }

        public int Number { get; set; }
        
        public Vector2 StartPosition { get; set; }

        public IObtainItemsInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IObtainItemsInfo WithIconKey(string iconKey)
        {
            IconKey = iconKey;
            return this;
        }

        public IObtainItemsInfo WithNumber(int number)
        {
            Number = number;
            return this;
        }

        public IObtainItemsInfo WithStartPosition(Vector2 position)
        {
            StartPosition = position;
            return this;
        }
    }
}