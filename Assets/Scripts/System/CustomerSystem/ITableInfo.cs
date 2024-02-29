using UnityEngine;

namespace daifuDemo
{
    public enum TableState
    {
        Empty,
        HavePerson,
        HaveRubbish
    }
    
    public interface ITableInfo
    {
        Vector2 CurrentPosition { get; }
        
        ITableInfo WithCurrentTransform(Vector2 transform);
    }

    public class TableInfo : ITableInfo
    {
        public Vector2 CurrentPosition { get; private set; }
        
        public ITableInfo WithCurrentTransform(Vector2 position)
        {
            CurrentPosition = position;
            return this;
        }
    }
}