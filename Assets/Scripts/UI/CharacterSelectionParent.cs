using System;
using UnityEngine;

public class CharacterSelectionParent : MonoBehaviour
{
    public static CharacterSelectionParent Instance;

    public AllCharacters allCharacters;

    [SerializeField] private GameObject windowToClose;
    [SerializeField] private GameObject windowToOpen;

    private void Awake()
    {
        Reset();
        Instance = this;
    }

    public void Reset()
    {
        foreach (CharacterInfo character in allCharacters.list)
        {
            character.used = false;
        }
    }

    public void CheckApproves()
    {
        int requiredApproves = Mathf.Max(2, transform.childCount);
        foreach (Transform child in transform)
        {
            CharacterSelection selection = child.GetComponent<CharacterSelection>();
            if (selection && selection.approved)
            {
                requiredApproves--;
            }
        }

        if (requiredApproves == 0)
        {
            windowToClose.SetActive(false);
            windowToOpen.SetActive(true);
        }
    }
}
