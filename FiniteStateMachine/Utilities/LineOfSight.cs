using UnityEditor;
using UnityEngine;
namespace Assets.FiniteStateMachine.Utilities
{
    public class LineOfSight : MonoBehaviour
    {
        [Tooltip("After how many frames should It Check for player, Don't modify if you don't know what are you doing")]
        [SerializeField]
        private int waitForFrames = 10; //For Optimization
        private int currentPassedFrames = 0;

        [Range(0, 360)]
        public int angle = 90;
        public float fovRadius = 10;
        public LayerMask targetMask;
        public LayerMask obstructionMask;

        public bool CanSeePlayer { get; private set; }
        public Transform Target { get; private set; }

        private void Update()
        {
            currentPassedFrames++;
            if (currentPassedFrames == waitForFrames)
            {
                currentPassedFrames = 0;
                FieldOfViewCheck();
            }
        }
        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, fovRadius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        CanSeePlayer = true;
                        Target = target;
                    }
                    else
                    {
                        CanSeePlayer = false;
                        Target = null;
                    }
                }
                else
                {
                    CanSeePlayer = false;
                    Target = null;
                }
            }
            else if (CanSeePlayer)
                CanSeePlayer = false;
        }
    }

    [CustomEditor(typeof(LineOfSight))]
    public class LineOfSightEditor : Editor
    {
        private void OnSceneGUI()
        {
            LineOfSight LOS = (LineOfSight)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(LOS.transform.position, Vector3.up, Vector3.forward, 360, LOS.fovRadius);

            Vector3 viewAngle01 = DirectionFromAngle(LOS.transform.eulerAngles.y, -LOS.angle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(LOS.transform.eulerAngles.y, LOS.angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(LOS.transform.position, LOS.transform.position + viewAngle01 * LOS.fovRadius);
            Handles.DrawLine(LOS.transform.position, LOS.transform.position + viewAngle02 * LOS.fovRadius);

            if (LOS.CanSeePlayer)
            {
                Handles.color = Color.red;
                Handles.DrawLine(LOS.transform.position, LOS.Target.transform.position);
            }

        }
        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}