using UnityEngine;
using UnityEngine.UI;

public class ScenePreview : MonoBehaviour
{
    [SerializeField] private Image preview;
    [SerializeField] private Image selectionStamp;

    [HideInInspector]
    public SceneData sceneData;
    
    public void SetPreview(SceneData sceneData)
    {
        this.sceneData = sceneData;
        preview.sprite = sceneData.graphic;
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