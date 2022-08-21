using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(CharacterMovementInput))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float walkAcceleration = 5;
        [SerializeField] private float walkDeceleration = 5;
        [SerializeField] private LayerMask blockingLayers;
        
        private const int ResolveCollisionsIterationCount = 2;
        
        private CircleCollider2D _circleCollider2D;
        private CharacterMovementInput _input;
        private Vector3 _velocity;
        private Quaternion _targetRotation;

        private void Awake()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _input = GetComponent<CharacterMovementInput>();

            _targetRotation = transform.rotation;
        }

        private void Update()
        {
            var input = _input.NormalizedDirection;

            if (input.magnitude != 0)
            {
                _velocity = Vector3.MoveTowards(_velocity, speed * input, 
                    walkAcceleration * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, 
                    walkDeceleration * Time.deltaTime);
            }
            
            transform.position += _velocity * Time.deltaTime;

            ResolveCollisions();
        }

        private void ResolveCollisions()
        {
            for (var i = 0; i < ResolveCollisionsIterationCount; i++)
            {
                var hits = Physics2D.OverlapCircleAll(transform.position, 
                    _circleCollider2D.radius, blockingLayers);

                foreach (var hit in hits)
                {
                    if (hit == _circleCollider2D)
                        continue;
                    
                    var colliderDistance = hit.Distance(_circleCollider2D);

                    var translation = new Vector3(
                        colliderDistance.pointA.x - colliderDistance.pointB.x,
                        colliderDistance.pointA.y - colliderDistance.pointB.y, 0);
                        
                    if (colliderDistance.isOverlapped)
                    {
                        transform.position += translation;
                    }
                }
            }
        }
    }
}