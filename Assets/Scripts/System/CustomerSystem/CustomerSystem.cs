using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace daifuDemo
{
    public interface ICustomerSystem : ISystem
    {
        Dictionary<CustomerType, ICustomerInfo> CustomerInfos { get; }
        
        List<ICustomerItemInfo> CustomerItems { get; }
        
        List<ITableInfo> TableInfos { get; }
        
        List<ITableItemInfo> TableItems { get; }

        ICustomerSystem AddCustomerInfos(CustomerType type, ICustomerInfo customerInfo);

        ICustomerSystem AddTablesItems(ITableInfo tableItem);

        void CreateCustomers();

        void CreateTables();
    }
    
    public class CustomerSystem : AbstractSystem, ICustomerSystem
    {
        private IBusinessModel _businessModel;
        
        protected override void OnInit()
        {
            _businessModel = this.GetModel<IBusinessModel>();
            
            this.AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-5, 2)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-3.5f, 2)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-2, 2)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-0.5f, 2)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(1, 2)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(2.5f, 2)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-5, -1)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-3.5f, -1)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-2, -1)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-0.5f, -1)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(1, -1)))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(2.5f, -1)));
            
            this.AddCustomerInfos(CustomerType.Woman, new CustomerInfo()
                    .WithWalkSpeed(10f)
                    .WithMaxWaitTime(20f)
                    .WithMinWaitTime(15f)
                    .WithMaxEatTime(10f)
                    .WithMinEatTime(7f)
                    .WithMaxTip(100f)
                    .WithMinTip(10f)
                    .WithDrinkProbability(0.2f)
                    .WithDrinkTipMultiple(0.3f))
                .AddCustomerInfos(CustomerType.Child, new CustomerInfo()
                    .WithWalkSpeed(15f)
                    .WithMaxWaitTime(30f)
                    .WithMinWaitTime(20f)
                    .WithMaxEatTime(13f)
                    .WithMinEatTime(5f)
                    .WithMaxTip(20f)
                    .WithMinTip(5f)
                    .WithDrinkProbability(0.4f)
                    .WithDrinkTipMultiple(0.1f))
                .AddCustomerInfos(CustomerType.Man, new CustomerInfo()
                    .WithWalkSpeed(13f)
                    .WithMaxWaitTime(40f)
                    .WithMinWaitTime(30f)
                    .WithMaxEatTime(7f)
                    .WithMinEatTime(4f)
                    .WithMaxTip(50f)
                    .WithMinTip(30f)
                    .WithDrinkProbability(0.5f)
                    .WithDrinkTipMultiple(0.2f))
                .AddCustomerInfos(CustomerType.OldMan, new CustomerInfo()
                    .WithWalkSpeed(5f)
                    .WithMaxWaitTime(50f)
                    .WithMinWaitTime(40f)
                    .WithMaxEatTime(12f)
                    .WithMinEatTime(7f)
                    .WithMaxTip(30f)
                    .WithMinTip(20f)
                    .WithDrinkProbability(0.1f)
                    .WithDrinkTipMultiple(0.2f));
        }

        public Dictionary<CustomerType, ICustomerInfo> CustomerInfos { get; } =
            new Dictionary<CustomerType, ICustomerInfo>();

        public List<ICustomerItemInfo> CustomerItems { get; } = new List<ICustomerItemInfo>();

        public List<ITableInfo> TableInfos { get; } = new List<ITableInfo>();

        public List<ITableItemInfo> TableItems { get; } = new List<ITableItemInfo>();

        public ICustomerSystem AddCustomerInfos(CustomerType type, ICustomerInfo customerInfo)
        {
            CustomerInfos.Add(type, customerInfo);
            return this;
        }

        public ICustomerSystem AddTablesItems(ITableInfo tableItem)
        {
            TableInfos.Add(tableItem);
            return this;
        }

        public void CreateCustomers()
        {
            CustomerItems.Clear();
            
            for (int i = 0; i < _businessModel.MaxCustomerNumber.Value; i++)
            {
                System.Random random = new System.Random();
                CustomerType customerType = (CustomerType)random.Next(Enum.GetValues(typeof(CustomerType)).Length);
                ICustomerInfo customerInfo = CustomerInfos[customerType];

                ICustomerItemInfo customerItem = new CustomerItemInfo()
                    .WithState(CustomerItemState.Free)
                    .WithWalkSpeed(customerInfo.WalkSpeed)
                    .WithWaitTime(Random.Range(customerInfo.MinWaitTime, customerInfo.MaxWaitTime))
                    .WithOrderNeedTime(Random.Range(1f, 5f))
                    .WithEatTime(Random.Range(customerInfo.MaxEatTime, customerInfo.MinEatTime))
                    .WithTip(Random.Range(customerInfo.MinTip, customerInfo.MaxTip))
                    .WithIfDrink(Random.Range(0, 1f) < customerInfo.DrinkProbability)
                    .WithTipMultiple(customerInfo.DrinkTipMultiple)
                    .WithIfReceiveOrderDish(false);
                
                CustomerItems.Add(customerItem);
            }
        }

        public void CreateTables()
        {
            TableItems.Clear();

            foreach (var tableInfo in TableInfos)
            {
                ITableItemInfo tableItem = new TableItemInfo()
                    .WithTableState(TableState.Empty)
                    .WithCurrentTransform(tableInfo.CurrentPosition)
                    .WithCustomerInfo(null);
                
                TableItems.Add(tableItem);
            }
        }
    }
}