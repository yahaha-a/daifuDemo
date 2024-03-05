using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface ICollectionModel : IModel
    {
        BindableProperty<float> Gold { get; }
        
        BindableProperty<float> RestaurantScore { get; }

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
        
        public void Storage()
        {
            PlayerPrefs.SetFloat("gold", Gold.Value);
            PlayerPrefs.SetFloat("restaurantScore", RestaurantScore.Value);
        }

        public void Load()
        {
            Gold.Value = PlayerPrefs.GetFloat("gold", 0);
            RestaurantScore.Value = PlayerPrefs.GetFloat("restaurantScore", 0);
        }
    }
}