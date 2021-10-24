using System.Collections.Generic;
using UnityEngine;

public class SceneSelection : MonoBehaviour
{
    public static SceneSelection Instance;
    
    [SerializeField] private AllScenes allScenes;
    [SerializeField] private ScenePreview scenePreview;

    [Space]
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private Transform previewsParent;
    
    private List<ScenePreview> previews = new List<ScenePreview>();
    private ScenePreview currentPreview;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        currentPreview = null;
        previews.Clear();
        foreach (Transform child in previewsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (SceneData sceneData in allScenes.list)
        {
            ScenePreview preview = Instantiate(scenePreview, previewsParent);
            if (!currentPreview)
            {
                currentPreview = preview;
                preview.Select();
            }
            preview.SetPreview(sceneData);
            previews.Add(preview);
        }
    }

    public void ChangeSelection(int amount)
    {
        if (!currentPreview) return;
        
        int id = previews.FindIndex(p => p == currentPreview);
        id = (id + amount + previews.Count) % previews.Count;
        
        currentPreview.Deselect();
        currentPreview = previews[id];
        currentPreview.Select();
    }

    public void Approve()
    {
        if (!currentPreview) return;
        
        GameManager.Instance.StartGame(currentPreview.sceneData);
        preGameCanvas.SetActive(false);
    }
}
