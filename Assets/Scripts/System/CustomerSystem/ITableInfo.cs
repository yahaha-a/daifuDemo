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
        
        TableState TableState { get; }
        
        ICustomerInfo CustomerInfo { get; }

        ITableInfo WithCurrentTransform(Vector2 transform);

        ITableInfo WithTableState(TableState tableState);

        ITableInfo WithCustomerInfo(CustomerInfo customerInfo);
    }

    public class TableInfo : ITableInfo
    {
        public Vector2 CurrentPosition { get; private set; }
        
        public TableState TableState { get; private set; }
        
        public ICustomerInfo CustomerInfo { get; private set; }

        public ITableInfo WithCurrentTransform(Vector2 position)
        {
            CurrentPosition = position;
            return this;
        }

        public ITableInfo WithTableState(TableState tableState)
        {
            TableState = tableState;
            return this;
        }

        public ITableInfo WithCustomerInfo(CustomerInfo customerInfo)
        {
            CustomerInfo = customerInfo;
            return this;
        }
    }
}