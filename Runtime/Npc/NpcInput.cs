using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    public class NpcInput : MonoBehaviour
    {
        [SerializeField] private Transform lookTarget;

        private Vector2 _dir;

        private void Awake()
        {
            lookTarget.SetParent(null); 
        }

        public void SetMoveDirection(Vector2 dir)
        {
            _dir = dir;
        }

        public void SetLookTarget(Vector3 target)
        {
            lookTarget.position = target;
        }
        
        public Vector3 GetLookTarget()
        {
            return lookTarget.position;
        }

        public Vector2 GetNormalizedMovement()
        {
            return _dir;
        }

        public bool IsAttacking()
        {
            return false;
        }
    }
}
