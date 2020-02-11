using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
  public void LoadStartMenu()
  {
    SceneManager.LoadScene(0);
  }

  public void LoadMainLevel()
  {
    SceneManager.LoadScene("Laser Shooter");
    FindObjectOfType<GameSession>().ResetGame();
  }


  private IEnumerator GameOver()
  {
    yield return new WaitForSeconds(3);
    SceneManager.LoadScene("Game Over");
  }
  public void LoadGameOver()
  {
    StartCoroutine(GameOver());
  }
  public void QuitGame()
  {
    Application.Quit();
  }
}
