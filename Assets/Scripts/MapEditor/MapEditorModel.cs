using Global;
using QFramework;
using UnityEngine;

namespace MapEditor
{
    public interface IMapEditorModel : IModel
    {
        BindableProperty<Vector3> CurrentMousePosition { get; }
        
        BindableProperty<OptionType> CurrentOptionType { get; }
        
        BindableProperty<CreateItemName> CurrentMapEditorName { get; }
        
        BindableProperty<float> CurrentSelectRange { get; }
        
        BindableProperty<int> CurrentSerialNumber { get; }
        
        BindableProperty<string> CurrentSelectArchiveName { get; }
        
        BindableProperty<string> CurrentArchiveName { get; }
        
        BindableProperty<int> CurrentCreateItemNumber { get; }
        
        BindableProperty<bool> IfInputCreateNumberPanelShow { get; }
    }
    
    public class MapEditorModel : AbstractModel, IMapEditorModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<Vector3> CurrentMousePosition { get; } = new BindableProperty<Vector3>(Vector3.zero);

        public BindableProperty<OptionType> CurrentOptionType { get; } =
            new BindableProperty<OptionType>(OptionType.Null);

        public BindableProperty<CreateItemName> CurrentMapEditorName { get; } =
            new BindableProperty<CreateItemName>(CreateItemName.Null);

        public BindableProperty<float> CurrentSelectRange { get; } = new BindableProperty<float>(100);

        public BindableProperty<int> CurrentSerialNumber { get; } = new BindableProperty<int>(1);

        public BindableProperty<string> CurrentSelectArchiveName { get; } = new BindableProperty<string>(null);
        
        public BindableProperty<string> CurrentArchiveName { get; } = new BindableProperty<string>(null);
        
        public BindableProperty<int> CurrentCreateItemNumber { get; } = new BindableProperty<int>(1);
        
        public BindableProperty<bool> IfInputCreateNumberPanelShow { get; } = new BindableProperty<bool>(false);
    }
}