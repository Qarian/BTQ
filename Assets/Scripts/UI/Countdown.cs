using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private SceneSelection sceneSelection;
    [SerializeField] private GameObject preGameCanvas;
    [SerializeField] private Image image;
    
    [SerializeField] private List<Sprite> counters;
    [SerializeField] private float countdownDelay = 1f;
    [SerializeField] private bool startOnLast;

    private bool started = false;

    private void Awake()
    {
        started = false;
        gameObject.SetActive(false);
    }

    public void StartCountdown()
    {
        IEnumerator Count()
        {
            for (int i = 0; i < counters.Count; i++)
            {
                image.sprite = counters[i];
                yield return new WaitForSeconds(countdownDelay);
                if (i == counters.Count - 1 && startOnLast)
                    EndCountdown();
            }
            EndCountdown();
            
            gameObject.SetActive(false);
            preGameCanvas.SetActive(false);
        }

        gameObject.SetActive(true);
        GameManager.Instance.InitArea(sceneSelection.selectedPreview.sceneData);
        StartCoroutine(Count());
    }

    private void EndCountdown()
    {
        if (started) return;

        started = true;
        GameManager.Instance.StartGame();
    }
}
