using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _endGameMenu;
    [SerializeField] private TMP_Text   _counterEnemyText;

    private float                       _defaultTimeScale;

    private void Awake()
    {
        _defaultTimeScale = Time.timeScale;
        Player.Instance.OnUnitDeath += EndGame;
    }

    public void EndGame(Unit unit)
    {
        Time.timeScale = 0;
        _counterEnemyText.text = $"Defeated {GameManager.Instance.EnemyCounter} Enemies";
        _endGameMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = _defaultTimeScale;
        GameManager.Instance.StartGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
    }

    public void BackToGame()
    {
        Time.timeScale = _defaultTimeScale;
        _pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = _defaultTimeScale;
        GameManager.Instance.BackToMainMenu();
    }
}
