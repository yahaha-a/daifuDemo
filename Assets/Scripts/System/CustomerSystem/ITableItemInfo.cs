using UnityEngine;

namespace daifuDemo
{
    public interface ITableItemInfo
    {
        Vector2 CurrentPosition { get; }
        
        TableState TableState { get; }
        
        ICustomerItemInfo CustomerItemInfo { get; }
        
        ITableItemInfo WithCurrentTransform(Vector2 transform);

        ITableItemInfo WithTableState(TableState tableState);
        
        ITableItemInfo WithCustomerInfo(ICustomerItemInfo customerItemInfo);

    }

    public class TableItemInfo: ITableItemInfo
    {
        public Vector2 CurrentPosition { get; private set; }
        
        public TableState TableState { get; private set; }
        
        public ICustomerItemInfo CustomerItemInfo { get; private set; }

        public ITableItemInfo WithCurrentTransform(Vector2 position)
        {
            CurrentPosition = position;
            return this;
        }

        public ITableItemInfo WithTableState(TableState tableState)
        {
            TableState = tableState;
            return this;
        }

        public ITableItemInfo WithCustomerInfo(ICustomerItemInfo customerItemInfo)
        {
            CustomerItemInfo = customerItemInfo;
            return this;
        }
    }
}