using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Image head;
    [SerializeField] private Image leg;
    [SerializeField] private TextMeshProUGUI characterName;
    
    [SerializeField] private AllCharacters allCharacters;

    [SerializeField] private HeadLeg player;
    private Character currentCharacter;

    public bool approved;

    public void Start()
    {
        currentCharacter = allCharacters.list.First(c => c.used == false);
        DisplayCharacter();
        
        transform.SetParent(CharacterSelectionParent.Instance.transform);
        transform.localScale = Vector3.one;
    }

    public void ChangeCharacter(int direction)
    {
        if (approved) return;
        
        currentCharacter.used = false;
        int id = allCharacters.list.FindIndex(c => c == currentCharacter);
        currentCharacter = null;

        if (direction > 0)
        {
            if (id < allCharacters.list.Count)
            {
                for (int i = id + 1; i < allCharacters.list.Count; i++)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        currentCharacter = allCharacters.list[i];
                        break;
                    }
                }
            }

            if (!currentCharacter)
            {
                for (int i = 0; i < id; i++)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        currentCharacter = allCharacters.list[i];
                        break;
                    }
                }
            }
        }
        else
        {
            if (id > 0)
            {
                for (int i = id - 1; i >= 0; i--)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        currentCharacter = allCharacters.list[i];
                        break;
                    }
                }
            }

            if (!currentCharacter)
            {
                for (int i = allCharacters.list.Count - 1; i > 0; i--)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        currentCharacter = allCharacters.list[i];
                        break;
                    }
                }
            }
        }

        DisplayCharacter();
    }

    private void DisplayCharacter()
    {
        currentCharacter.used = true;
        head.sprite = currentCharacter.head;
        leg.sprite = currentCharacter.leg;
        characterName.text = currentCharacter.name;
        characterName.color = Color.white;
    }

    public void Approve()
    {
        if (!currentCharacter) return;
        approved = true;
        characterName.color = Color.green;
        CharacterSelectionParent.Instance.CheckApproves();
        player.SetSprites(currentCharacter);
    }

    public void ResetApprove()
    {
        characterName.color = Color.white;
        approved = false;
    }
}
