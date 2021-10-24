using System.Collections.Generic;
using UnityEngine;

public class SceneSelection : MonoBehaviour
{
    public static SceneSelection Instance;
    
    [SerializeField] private AllScenes allScenes;
    [SerializeField] private ScenePreview scenePreview;

    [Space]
    [SerializeField] private Countdown countdown;
    [SerializeField] private Transform previewsParent;
    
    private List<ScenePreview> previews = new List<ScenePreview>();
    public ScenePreview selectedPreview { get; private set; }

    private bool closed = false;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        selectedPreview = null;
        previews.Clear();
        foreach (Transform child in previewsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (SceneData sceneData in allScenes.list)
        {
            ScenePreview preview = Instantiate(scenePreview, previewsParent);
            if (!selectedPreview)
            {
                selectedPreview = preview;
                preview.Select();
            }
            preview.SetPreview(sceneData);
            previews.Add(preview);
        }
    }

    public void ChangeSelection(int amount)
    {
        if (!selectedPreview) return;
        
        int id = previews.FindIndex(p => p == selectedPreview);
        id = (id + amount + previews.Count) % previews.Count;
        
        selectedPreview.Deselect();
        selectedPreview = previews[id];
        selectedPreview.Select();
    }

    public void Approve()
    {
        if (!selectedPreview || closed) return;

        closed = true;
        
        gameObject.SetActive(false);
        countdown.StartCountdown();
    }
}
