using UnityEngine;
using UnityEngine.AI;

public static class NavmeshUtilities
{
    public static Vector3 RandomNavmeshLocation(float radius, Transform from)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += from.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}
