using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshSampler
{
    public static Vector2 GetRandomPointInArc2D(Vector2 forwardDir, float radius, float angle)
    {
        float baseAngle = Mathf.Atan2(forwardDir.y, forwardDir.x);

        float halfAngleRad = (angle * 0.5f) * Mathf.Deg2Rad;
        float randomAngle = baseAngle + Random.Range(-halfAngleRad, halfAngleRad);

        return new Vector2(radius * Mathf.Cos(randomAngle), radius * Mathf.Sin(randomAngle));
    }

    public static List<Vector3> FindPointsInRadius(Vector3 center, Vector3 forwardVector, float radius, float angle, int sampleCount, int areaMask = NavMesh.AllAreas)
    {
        List<Vector3> validPoints = new List<Vector3>();

        for (int i = 0; i < sampleCount; i++)
        {
            Vector2 forward2D = new Vector2(forwardVector.x, forwardVector.z);
            Vector2 randomPoint2D = GetRandomPointInArc2D(forward2D, radius, angle);
            Vector3 randomPoint = center + new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, float.MaxValue, areaMask))
            {
                if (!validPoints.Contains(hit.position))
                {
                    validPoints.Add(hit.position);
                }
            }
        }
        return validPoints;
    }
}
