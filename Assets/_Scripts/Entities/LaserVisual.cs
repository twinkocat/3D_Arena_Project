using System.Collections;
using UnityEngine;

public class LaserVisual : MonoBehaviour
{
    private LineRenderer    _lineRenderer;
    public Vector3[]        points = new Vector3[2];
    public float            laserDuration;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, points[0]);
        _lineRenderer.SetPosition(1, points[1]);

        StartCoroutine(LaserVisualization());
    }


    private IEnumerator LaserVisualization()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        
        _lineRenderer.enabled = false;
        Destroy(gameObject);
    }

}
