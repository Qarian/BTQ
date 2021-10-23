using UnityEngine;
using UnityEngine.UI;

public class ScenePreview : MonoBehaviour
{
    [SerializeField] private Image preview;
    [SerializeField] private Image selectionStamp;
    

    public void SetPreview(SceneData sceneData)
    {
        preview.sprite = sceneData.preview;
    }

    public void Select()
    {
        selectionStamp.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        selectionStamp.gameObject.SetActive(false);
    }
}