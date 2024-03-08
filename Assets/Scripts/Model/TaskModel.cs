using QFramework;

namespace daifuDemo
{
    public interface ITaskModel : IModel
    {
        BindableProperty<ITaskInfo> CurrentTask { get; }
    }
    
    public class TaskModel : AbstractModel, ITaskModel
    {
        protected override void OnInit()
        {
            
        }
        
        public BindableProperty<ITaskInfo> CurrentTask { get; } = new BindableProperty<ITaskInfo>(null);
    }
}