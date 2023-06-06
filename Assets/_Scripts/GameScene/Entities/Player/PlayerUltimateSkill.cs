using UnityEngine;
using UnityEngine.UI;

public class PlayerUltimateSkill : MonoBehaviour
{
    [SerializeField] private Button _ultimateButton;
    [SerializeField] private Slider _energySlider;

    private float                   _currentEnergy;

    public void CheckCurrentEnergy()
    {
        _currentEnergy = _energySlider.value;

        if (_currentEnergy != Player.Instance.MaxEnergy)
        {
            _ultimateButton.interactable = false;
        }
        else
        {
            _ultimateButton.interactable = true;
        }
    }

    public void ActivateUltimate()
    {
        EnemySpawner.Instance.KillAllEnemies();
        Player.Instance.ChangeEnergyValue(-Player.Instance.MaxEnergy);
    }

}
