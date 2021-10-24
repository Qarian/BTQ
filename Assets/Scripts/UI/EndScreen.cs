using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private bool gameFinished = false;

    public void EndGame(CharacterInfo winner)
    {
        gameFinished = true;
        gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        if (!gameFinished) return;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
