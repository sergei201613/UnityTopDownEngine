using TeaGames.Unity.Utils.Runtime;
using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterLookTargetInput))]
    public class RotateToLookTarget : MonoBehaviour
    {
        [SerializeField] private float maxAngle = 20;

        private CharacterLookTargetInput _input;

        private void Update()
        {
            var dir = _input.LookTarget - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            var localEulerAngles = transform.localEulerAngles;
            
            localEulerAngles = new Vector3(
                localEulerAngles.x,
                localEulerAngles.y,
                Mathf.Clamp(localEulerAngles.z, 0, maxAngle));
            
            transform.localEulerAngles = localEulerAngles;
        }
    }
}
