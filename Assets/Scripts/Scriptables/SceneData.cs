using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Data", fileName = "Scene Data")]
public class SceneData : ScriptableObject
{
    public Sprite graphic;
    public GameObject sceneColliders;
    public Vector2[] spawnPositions;
}
