using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Transform heartsContainer;

    [Space]
    [SerializeField] private GameObject heart;
    
    public void SetUI(CharacterInfo characterInfo, int lives)
    {
        icon.sprite = characterInfo.head;
        characterName.text = characterInfo.name;
        for (int i = 0; i < lives; i++)
        {
            Instantiate(heart, heartsContainer);
        }
    }

    public void RemoveHeart()
    {
        Destroy(heartsContainer.GetChild(0).gameObject);
    }
}
