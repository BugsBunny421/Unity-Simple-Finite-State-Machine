using UnityEngine;
using UnityEngine.AI;
namespace Assets.FiniteStateMachine.Utilities
{
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
        public static bool canSeePlayer(float radius, Transform myPosition)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (Vector3.Distance(player.position, myPosition.position) <= radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
