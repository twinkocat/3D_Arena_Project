using System.Collections;
using UnityEngine;

public class CameraStartGameAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private float      _transitionTime;
    [SerializeField] private float      _areaForSecondTransitionPoint;
    [SerializeField] private float      _heightForSecondTransitionPoint;

    private MenuCameraRotator           _rotatorScript;
    private Vector3                     _secondTransitionPointOffset;
    private Vector3[]                   _leapPoints;

    private void Start()
    {
        _rotatorScript = GetComponent<MenuCameraRotator>();

        _secondTransitionPointOffset = new Vector3(
                                        Random.Range(-_areaForSecondTransitionPoint, _areaForSecondTransitionPoint), 
                                        _heightForSecondTransitionPoint, 
                                        Random.Range(-_areaForSecondTransitionPoint, _areaForSecondTransitionPoint));
        
        _leapPoints = new Vector3[3];
    }

    public void StartTransition()
    {
        _rotatorScript.enabled = false;

        _leapPoints[0] = transform.position;
        _leapPoints[1] = _endPoint.transform.position + _secondTransitionPointOffset;   // second point around player
        _leapPoints[2] = _endPoint.transform.position - Vector3.forward * 0.5f;         // players back

        float startTime = Time.time;
        float u = 0f;

        StartCoroutine(AnimationCoro(startTime, _transitionTime, u));
    }

    IEnumerator AnimationCoro(float startTime, float moveTime, float u)
    {
        while (u < 1)
        {
            transform.LookAt(_endPoint.transform.position);

            Vector3 p01, p12;
            u = Mathf.Min(((Time.time - startTime) / moveTime), 1);

            p01 = (1 - u) * _leapPoints[0] + u * _leapPoints[1];
            p12 = (1 - u) * _leapPoints[1] + u * _leapPoints[2];
            transform.position = (1 - u) * p01 + u * p12;

            yield return new WaitForFixedUpdate();
        }
        GameManager.Instance.StartGame();
    }
}
