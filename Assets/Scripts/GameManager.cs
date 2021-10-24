using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private GameObject gameCanvas;
    public EndScreen endGameCanvas;
    [SerializeField] private Transform playerUIParent;
    
    

    public static GameManager Instance;

    private List<HeadLeg> players = new List<HeadLeg>();

    private bool startedGame = false;

    private void Awake()
    {
        Instance = this;
        gameCanvas.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
    }

    public void StartGame(SceneData selectedScene)
    {
        if (startedGame) return;

        startedGame = true;
        
        Instantiate(selectedScene.sceneColliders, transform);
        background.sprite = selectedScene.graphic;
        gameCanvas.gameObject.SetActive(true);
        
        players = FindObjectsOfType<HeadLeg>().ToList();
        foreach (var player in players)
        {
            player.StartRound(selectedScene, playerUIParent);
        }
    }

    public void RemovedPlayer(HeadLeg player)
    {
        players.Remove(player);
        if (players.Count == 1)
            EndGame(players[0]);
    }

    private void EndGame(HeadLeg winner)
    {
        gameCanvas.SetActive(false);
        endGameCanvas.EndGame(winner.CharacterInfo);
    }
}
