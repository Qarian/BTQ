using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "All Scenes", fileName = "All Scenes")]
public class AllScenes : ScriptableObject
{
    public List<SceneData> list;
}