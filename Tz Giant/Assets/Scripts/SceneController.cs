using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _fixedJoystick;
    private float _delayEndGame = 4f;

    private void OnEnable()
    {
        ChestAnimation.OnWinEvent += ActiveEndScreen;
        EnemyPartsForCheckPlayer.OnLosedEvent += ActiveEndScreen;
    }

    private void OnDisable()
    {
        ChestAnimation.OnWinEvent -= ActiveEndScreen;
        EnemyPartsForCheckPlayer.OnLosedEvent -= ActiveEndScreen;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ActiveEndScreen()
    {
        StartCoroutine(ActiveEndScreenCor());
    }

    IEnumerator ActiveEndScreenCor()
    {
        
        yield return new WaitForSeconds(_delayEndGame);
        _fixedJoystick.SetActive(false);
        _winScreen.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void ReloadCurrentLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
