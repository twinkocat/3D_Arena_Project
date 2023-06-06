using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private float                       _defaultTimeScale;

    private void Awake()
    {
        _defaultTimeScale = Time.timeScale;
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
