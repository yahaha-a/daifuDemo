using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

// using Random = System.Random;

namespace daifuDemo
{
    public interface ICustomerSystem : ISystem
    {
        Dictionary<CustomerType, ICustomerInfo> CustomerInfos { get; }
        
        List<ICustomerItemInfo> CustomerItems { get; }
        
        List<ITableInfo> TableItems { get; }

        ICustomerSystem AddCustomerInfos(CustomerType type, ICustomerInfo customerInfo);

        ICustomerSystem AddTablesItems(ITableInfo tableItem);

        void CreateCustomers();
    }
    
    public class CustomerSystem : AbstractSystem, ICustomerSystem
    {
        private IBusinessModel _businessModel;
        
        protected override void OnInit()
        {
            _businessModel = this.GetModel<IBusinessModel>();
            
            this.AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-5, 2))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-3.5f, 2))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-2, 2))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-0.5f, 2))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(1, 2))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(2.5f, 2))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-5, -1))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-3.5f, -1))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-2, -1))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(-0.5f, -1))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(1, -1))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null))
                .AddTablesItems(new TableInfo()
                    .WithCurrentTransform(new Vector2(2.5f, -1))
                    .WithTableState(TableState.Empty)
                    .WithCustomerInfo(null));
            
            this.AddCustomerInfos(CustomerType.Woman, new CustomerInfo()
                    .WithWalkSpeed(10f)
                    .WithMaxWaitTime(20f)
                    .WithMinWaitTime(15f)
                    .WithMaxTip(100f)
                    .WithMinTip(10f)
                    .WithDrinkProbability(0.2f)
                    .WithDrinkTipMultiple(0.3f))
                .AddCustomerInfos(CustomerType.Child, new CustomerInfo()
                    .WithWalkSpeed(15f)
                    .WithMaxWaitTime(30f)
                    .WithMinWaitTime(20f)
                    .WithMaxTip(20f)
                    .WithMinTip(5f)
                    .WithDrinkProbability(0.4f)
                    .WithDrinkTipMultiple(0.1f))
                .AddCustomerInfos(CustomerType.Man, new CustomerInfo()
                    .WithWalkSpeed(13f)
                    .WithMaxWaitTime(40f)
                    .WithMinWaitTime(30f)
                    .WithMaxTip(50f)
                    .WithMinTip(30f)
                    .WithDrinkProbability(0.5f)
                    .WithDrinkTipMultiple(0.2f))
                .AddCustomerInfos(CustomerType.OldMan, new CustomerInfo()
                    .WithWalkSpeed(5f)
                    .WithMaxWaitTime(50f)
                    .WithMinWaitTime(40f)
                    .WithMaxTip(30f)
                    .WithMinTip(20f)
                    .WithDrinkProbability(0.1f)
                    .WithDrinkTipMultiple(0.2f));
        }

        public Dictionary<CustomerType, ICustomerInfo> CustomerInfos { get; } =
            new Dictionary<CustomerType, ICustomerInfo>();

        public List<ICustomerItemInfo> CustomerItems { get; } = new List<ICustomerItemInfo>();

        public List<ITableInfo> TableItems { get; } = new List<ITableInfo>();

        public ICustomerSystem AddCustomerInfos(CustomerType type, ICustomerInfo customerInfo)
        {
            CustomerInfos.Add(type, customerInfo);
            return this;
        }

        public ICustomerSystem AddTablesItems(ITableInfo tableItem)
        {
            TableItems.Add(tableItem);
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
                    .WithTip(Random.Range(customerInfo.MinTip, customerInfo.MaxTip))
                    .WithIfDrink(Random.Range(0, 1f) < customerInfo.DrinkProbability)
                    .WithTipMultiple(customerInfo.DrinkTipMultiple);
                
                CustomerItems.Add(customerItem);
            }
        }
    }
}