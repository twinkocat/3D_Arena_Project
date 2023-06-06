using UnityEngine;

public class MenuCameraRotator : MonoBehaviour
{
    [SerializeField] private GameObject _pointOfInterest;
    [SerializeField] private float      _moveSpeed;

    private void Update()
    {
        transform.LookAt(_pointOfInterest.transform);
        transform.position += transform.right * _moveSpeed * Time.deltaTime;
    }
}
