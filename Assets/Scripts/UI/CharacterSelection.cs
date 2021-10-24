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
    private CharacterInfo _currentCharacterInfo;

    public bool approved;

    public void Start()
    {
        _currentCharacterInfo = allCharacters.list.First(c => c.used == false);
        DisplayCharacter();
        
        transform.SetParent(CharacterSelectionParent.Instance.transform);
        transform.localScale = Vector3.one;
    }

    public void ChangeCharacter(int direction)
    {
        if (approved) return;
        
        _currentCharacterInfo.used = false;
        int id = allCharacters.list.FindIndex(c => c == _currentCharacterInfo);
        _currentCharacterInfo = null;

        if (direction > 0)
        {
            if (id < allCharacters.list.Count)
            {
                for (int i = id + 1; i < allCharacters.list.Count; i++)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        _currentCharacterInfo = allCharacters.list[i];
                        break;
                    }
                }
            }

            if (!_currentCharacterInfo)
            {
                for (int i = 0; i < id; i++)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        _currentCharacterInfo = allCharacters.list[i];
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
                        _currentCharacterInfo = allCharacters.list[i];
                        break;
                    }
                }
            }

            if (!_currentCharacterInfo)
            {
                for (int i = allCharacters.list.Count - 1; i > 0; i--)
                {
                    if (allCharacters.list[i].used == false)
                    {
                        _currentCharacterInfo = allCharacters.list[i];
                        break;
                    }
                }
            }
        }

        DisplayCharacter();
    }

    private void DisplayCharacter()
    {
        _currentCharacterInfo.used = true;
        head.sprite = _currentCharacterInfo.head;
        leg.sprite = _currentCharacterInfo.leg;
        characterName.text = _currentCharacterInfo.name;
        characterName.color = Color.white;
    }

    public void Approve()
    {
        if (!_currentCharacterInfo) return;
        approved = true;
        characterName.color = new Color(0.01176471f, 0.2039216f, 0.4823529f);
        CharacterSelectionParent.Instance.CheckApproves();
        player.SetCharacter(_currentCharacterInfo);
    }

    public void ResetApprove()
    {
        characterName.color = Color.white;
        approved = false;
    }
}
