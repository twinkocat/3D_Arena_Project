using UnityEngine;


public class CameraHolder : MonoBehaviour
{
    [SerializeField] private GameObject _pointOfInterest;   // player is a default POI for camera holder
    [SerializeField] private float      _offsetY;           // headY position 0.06f - 0.08f
    [SerializeField] private float      _offsetZ;           // headX position 0.06f - 0.08f

    private void Awake()
    {
        if (_pointOfInterest == null)
        {
            Debug.Log("Set target to camera follow");
            return;
        }
    }

    private void Start()
    {
        transform.parent = _pointOfInterest.transform;
    }

    private void LateUpdate()
    {
        transform.position = _pointOfInterest.transform.position + new Vector3(0f, _offsetY, _offsetZ);   
    }
}
