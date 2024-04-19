using System.Collections.Generic;
using UnityEngine;

namespace TestTask.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Config", menuName = "Data/Config")]
    public class Config : ScriptableObject
    {
        [field: SerializeField] public List<string> Cities = new List<string>();
    }
}