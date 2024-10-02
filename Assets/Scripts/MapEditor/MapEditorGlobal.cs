using daifuDemo;
using QFramework;

namespace MapEditor
{
    public class MapEditorGlobal : Architecture<MapEditorGlobal>
    {
        protected override void Init()
        {
            RegisterUtility<IUtils>(new Utils());
            RegisterModel<IMapEditorModel>(new MapEditorModel());
            RegisterSystem<IMapEditorSystem>(new MapEditorSystem());
        }
    }
}