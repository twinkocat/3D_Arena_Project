using System.Collections;
using UnityEngine;

public class LaserVisual : MonoBehaviour
{
    [SerializeField] private float              _laserDuration;
    private LineRenderer                        _lineRenderer;
    public Vector3[]                            Points = new Vector3[2];

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.SetPosition(0, Points[0]);
        _lineRenderer.SetPosition(1, Points[1]);

        StartCoroutine(LaserVisualization());
    }


    private IEnumerator LaserVisualization()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(_laserDuration);
        
        _lineRenderer.enabled = false;
        Destroy(gameObject);
    }

}
