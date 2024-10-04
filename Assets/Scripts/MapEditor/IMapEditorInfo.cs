using Global;
using UnityEngine;

namespace MapEditor
{
    public enum OptionType
    {
        Null,
        Single,
        Range
    }
    
    public interface IMapEditorInfo
    {
        CreateItemName Key { get; }
        
        OptionType OptionType { get; }
        
        CreateItemType CreateItemType { get; }
        
        string Name { get; }
        
        IMapEditorInfo WithKey(CreateItemName key);

        IMapEditorInfo WithOptionType(OptionType type);

        IMapEditorInfo WithCreateItemType(CreateItemType type);

        IMapEditorInfo WithName(string name);
    }

    public class MapEditorInfo : IMapEditorInfo
    {
        public CreateItemName Key { get; set; }
        
        public OptionType OptionType { get; set; }
        
        public CreateItemType CreateItemType { get; set; }
        
        public string Name { get; set; }
        
        public IMapEditorInfo WithKey(CreateItemName key)
        {
            Key = key;
            return this;
        }

        public IMapEditorInfo WithOptionType(OptionType type)
        {
            OptionType = type;
            return this;
        }

        public IMapEditorInfo WithCreateItemType(CreateItemType type)
        {
            CreateItemType = type;
            return this;
        }

        public IMapEditorInfo WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}