using QFramework;
using UnityEngine;

namespace MapEditor
{
    public enum MapEditorName
    {
        Null,
        Kelp,
        NormalTreasureChest,
        NormalFishEditor
    }
    
    public interface IMapEditorModel : IModel
    {
        BindableProperty<Vector3> CurrentMousePosition { get; }
        
        BindableProperty<MapEditorName> CurrentMapEditorName { get; }
        
        BindableProperty<float> CurrentSelectRange { get; }
        
        BindableProperty<int> CurrentSerialNumber { get; }
        
        BindableProperty<string> CurrentSelectArchiveName { get; }
        
        BindableProperty<string> CurrentArchiveName { get; }
    }
    
    public class MapEditorModel : AbstractModel, IMapEditorModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<Vector3> CurrentMousePosition { get; } = new BindableProperty<Vector3>(Vector3.zero);

        public BindableProperty<MapEditorName> CurrentMapEditorName { get; } =
            new BindableProperty<MapEditorName>(MapEditorName.Null);

        public BindableProperty<float> CurrentSelectRange { get; } = new BindableProperty<float>(100);

        public BindableProperty<int> CurrentSerialNumber { get; } = new BindableProperty<int>(1);

        public BindableProperty<string> CurrentSelectArchiveName { get; } = new BindableProperty<string>(null);
        
        public BindableProperty<string> CurrentArchiveName { get; } = new BindableProperty<string>(null);
    }
}