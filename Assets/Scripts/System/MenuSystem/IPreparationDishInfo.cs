namespace daifuDemo
{
    public enum PreparationDishState
    {
        NotStart,
        Making,
        Finish
    }
    
    public interface IPreparationDishInfo
    {
        string Key { get; }
        
        float MakeNeedTime { get; set; }
        
        IPreparationDishInfo WithKey(string key);

        IPreparationDishInfo WithMakeNeedTime(float makeNeedTime);
    }

    public class PreparationDishInfo : IPreparationDishInfo
    {
        public string Key { get; private set; }
        
        public float MakeNeedTime { get; set; }

        public IPreparationDishInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IPreparationDishInfo WithMakeNeedTime(float makeNeedTime)
        {
            MakeNeedTime = makeNeedTime;
            return this;
        }
    }
}