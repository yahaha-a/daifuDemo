using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface ICollectionModel : IModel
    {
        BindableProperty<float> Gold { get; }
        
        BindableProperty<float> RestaurantScore { get; }
        
        BindableProperty<int> ReceptionCustomerTotalAmount { get; }

        void Storage();

        void Load();
    }
    
    public class CollectionModel : AbstractModel, ICollectionModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<float> Gold { get; } = new BindableProperty<float>(0);

        public BindableProperty<float> RestaurantScore { get; } = new BindableProperty<float>(0);
        
        public BindableProperty<int> ReceptionCustomerTotalAmount { get; } = new BindableProperty<int>(0);

        public void Storage()
        {
            PlayerPrefs.SetFloat("gold", Gold.Value);
            PlayerPrefs.SetFloat("restaurantScore", RestaurantScore.Value);
            PlayerPrefs.SetInt("receptionCustomerTotalAmount", ReceptionCustomerTotalAmount.Value);
        }

        public void Load()
        {
            Gold.Value = PlayerPrefs.GetFloat("gold", 0);
            RestaurantScore.Value = PlayerPrefs.GetFloat("restaurantScore", 0);
            ReceptionCustomerTotalAmount.Value = PlayerPrefs.GetInt("receptionCustomerTotalAmount", 0);
        }
    }
}