using UnityEngine;

[CreateAssetMenu(menuName = "New Character", fileName = "Character")]
public class CharacterInfo : ScriptableObject
{
    public Sprite head;
    public Sprite leg;
    public new string name;
    [HideInInspector]
    public bool used = false;
}
