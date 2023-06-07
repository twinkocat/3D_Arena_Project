using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager   Instance;  // Instance

    public float                MAX_SENS = 120f;
    public float                MIN_SENS = 40f;

    private int                 _counterCurrentDeadEnemies;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        ResetCounterEnemies();

        DontDestroyOnLoad(gameObject);
        
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        if (!PlayerPrefs.HasKey(nameof(SensX)))
        {
            PlayerPrefs.SetFloat(nameof(SensX), MIN_SENS);
        }
        if (!PlayerPrefs.HasKey(nameof(SensY)))
        {
            PlayerPrefs.SetFloat(nameof(SensY), MIN_SENS);
        }
    }

    public void SaveSens(string varName, float value)
    {
        PlayerPrefs.SetFloat(varName, value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CounterEnemies(Unit unit)
    {
        CurrentDeadEnemyCounter++;
    }

    public void ResetCounterEnemies()
    {
        CurrentDeadEnemyCounter = 0;
    }

    public int CurrentDeadEnemyCounter
    {
        get { return _counterCurrentDeadEnemies; }
        private set { _counterCurrentDeadEnemies = value; }
    }

    public float SensX
    {
        get { return PlayerPrefs.GetFloat(nameof(SensX)); }
    }

    public float SensY
    {
        get { return PlayerPrefs.GetFloat(nameof(SensY)); }
    }
}
