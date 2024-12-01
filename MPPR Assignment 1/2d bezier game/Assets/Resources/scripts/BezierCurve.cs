using UnityEngine;
using System.Collections.Generic;

public class BezierCurve : MonoBehaviour
{
    [Header("Control Points")]
    public List<Transform> controlPoints;

    [HideInInspector]
    public float resolution = 0.033f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    void Update()
    {
        List<Vector3> curvePoints = new List<Vector3>();
        for (float t = 0; t <= 1; t += resolution)
        {
            curvePoints.Add(CalculateBezierPoint(t, controlPoints));
        }

        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    void OnDrawGizmos()
    {
        if (controlPoints == null || controlPoints.Count < 2)
            return;

        Vector3 previousPoint = controlPoints[0].position;

        for (float t = 0; t <= 1; t += resolution)
        {
            Vector3 pointOnCurve = CalculateBezierPoint(t, controlPoints);
            Gizmos.DrawLine(previousPoint, pointOnCurve);
            previousPoint = pointOnCurve;
        }
    }

    public Vector3 CalculateBezierPoint(float t, List<Transform> points)
    {
        List<Vector3> currentPoints = new List<Vector3>();

        foreach (var p in points)
        {
            currentPoints.Add(p.position);
        }

        while (currentPoints.Count > 1)
        {
            List<Vector3> newPoints = new List<Vector3>();
            for (int i = 0; i < currentPoints.Count - 1; i++)
            {
                newPoints.Add(Interpolation.Lerp(currentPoints[i], currentPoints[i + 1], t));
            }
            currentPoints = newPoints;
        }

        return currentPoints[0];
    }
}
