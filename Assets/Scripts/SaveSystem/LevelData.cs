using System.Collections.Generic;
using TestTask.Enums;

namespace TestTask.Save
{
    [System.Serializable]
    public class LevelData
    {
        public int SceneId { get; set; }
        public List<string> Cities { get; set; } = new List<string>();
        public WindowEnum WindowEnum { get; set; }
    }
}

