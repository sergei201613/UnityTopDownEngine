using Unity.Netcode;
using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class CharacterMovement : NetworkBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float walkAcceleration = 5;
        [SerializeField] private float walkDeceleration = 5;
        [SerializeField] private LayerMask blockingLayers;
        [SerializeField] private int _resolveCollisionsIterationCount = 2;
        [SerializeField] private float _moveInterpSpeed = 5f;
        
        private CircleCollider2D _circleCollider2D;
        private CharacterMovementInput _movementInputOwnerOnly;
        private Vector3 _velocity;

        private readonly NetworkVariable<Vector3> _netPosition = new(
            writePerm: NetworkVariableWritePermission.Server);

        private readonly NetworkVariable<Vector2> _netInputDir = new(
            writePerm: NetworkVariableWritePermission.Owner);

        private void Awake()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _movementInputOwnerOnly = GetComponent<CharacterMovementInput>();
        }

        private void Update()
        {
            if (IsOwner)
            {
                _netInputDir.Value = _movementInputOwnerOnly.NormalizedDirection;
            }

            if (IsServer)
            {
                Move(_netInputDir.Value.normalized);
                ResolveCollisions();
                _netPosition.Value = transform.position;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, 
                    _netPosition.Value, _moveInterpSpeed * Time.deltaTime);

                Move(_netInputDir.Value);
                ResolveCollisions();
            }
        }

        private void Move(Vector2 ownerInput)
        {
            if (ownerInput.magnitude != 0)
            {
                _velocity = Vector3.MoveTowards(_velocity, speed * ownerInput,
                    walkAcceleration * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.MoveTowards(_velocity, Vector3.zero,
                    walkDeceleration * Time.deltaTime);
            }

            transform.position += _velocity * Time.deltaTime;
        }

        private void ResolveCollisions()
        {
            for (var i = 0; i < _resolveCollisionsIterationCount; i++)
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