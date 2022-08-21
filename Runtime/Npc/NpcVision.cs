using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    public class NpcVision : MonoBehaviour
    {
        [SerializeField] private int raysCount = 32;
        [SerializeField] private float rayDistance = 10;
        [SerializeField] private float viewAngle = 45;

        private void Update()
        {
            for (int i = 0; i < raysCount; i++)
            {
                int sections = raysCount - 1;
                float deltaAngle = viewAngle / sections;
                float angle = transform.eulerAngles.z + (i * deltaAngle) + 
                    (90 - (sections / 2f * deltaAngle));

                angle *= Mathf.Deg2Rad;

                Vector3 dir = new(Mathf.Cos(angle), Mathf.Sin(angle));
                var hit = Physics2D.Raycast(transform.position, dir, rayDistance);

#if UNITY_EDITOR
                Debug.DrawLine(transform.position, 
                    hit ? hit.point : transform.position + dir * rayDistance,
                    hit ? Color.red : Color.white);
#endif
            }
        }
    }
}
