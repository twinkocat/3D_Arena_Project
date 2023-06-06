using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private CameraStartGameAnimation _cameraStart;
    [SerializeField] private GameObject     _mainMenuHolder;
    [Space]
    [SerializeField] private Slider         _xSensSlider;
    [SerializeField] private Slider         _ySensSlider;

    public void StartGame()
    {
        _mainMenuHolder.SetActive(false);
        _cameraStart.StartTransition();
    }




    public void Exit()
    {
        Application.Quit();
    }
}
