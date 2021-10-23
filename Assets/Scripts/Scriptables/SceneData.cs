using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Data", fileName = "Scene Data")]
public class SceneData : ScriptableObject
{
    public Scene scene;
    public Sprite preview;
    public Vector2[] spawnPositions;
}
