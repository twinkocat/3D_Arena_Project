using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        transform.LookAt(mainCameraTransform.position);
    }
}
