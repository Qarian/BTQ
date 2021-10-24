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

    private bool createdArea = false;
    private bool startedGame = false;

    private void Awake()
    {
        Instance = this;
        gameCanvas.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);

        PlayerInput.disabledHeadLegInput = true;
    }

    public void InitArea(SceneData selectedScene)
    {
        if (createdArea) return;

        createdArea = true;
        Instantiate(selectedScene.sceneColliders, transform);
        background.sprite = selectedScene.graphic;
        
        players = FindObjectsOfType<HeadLeg>().ToList();
        foreach (var player in players)
        {
            player.ReadyHeadLegs(selectedScene);
        }
    }

    public void StartGame()
    {
        if (startedGame) return;

        startedGame = true;

        PlayerInput.disabledHeadLegInput = false;
        
        gameCanvas.gameObject.SetActive(true);
        
        foreach (var player in players)
        {
            player.StartRound(playerUIParent);
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
