using System.Collections;
using Pathfinding;
using TeaGames.Unity.GameplayFramework.Runtime;
using TeaGames.Unity.Utils.Runtime;
using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    public class NpcController : AIController
    {
        [SerializeField] private Transform target;
        [SerializeField] private float nextPointDist = 2;
        [SerializeField] private float minPathUpdateRate = .2f;
        [SerializeField] private float maxPathUpdateRate = .7f;
        
        private Seeker _seeker;
        private Path _path;
        private CharacterMovementInput _movementInput;
        private CharacterLookTargetInput _lookTargetInput;
        private int _curPoint = 0;

        private void Awake()
        {
            _seeker = this.RequireComponent<Seeker>();
            _movementInput = this.RequireComponent<CharacterMovementInput>();
            _lookTargetInput = this.RequireComponent<CharacterLookTargetInput>();

            StartCoroutine(UpdatePathCoroutine());
        }

        private void Update()
        {
            HandlePathMovement();
        }

        private void HandlePathMovement()
        {
            if (_curPoint >= _path.vectorPath.Count)
            {
                _movementInput.NormalizedDirection = Vector2.zero;
                return; 
            }

            var curPoint = _path.vectorPath[_curPoint];
            var dir = curPoint - transform.position;

            _movementInput.NormalizedDirection = dir;
            _lookTargetInput.LookTarget = curPoint;

            var dist = Vector2.Distance(transform.position, curPoint);

            if (dist < nextPointDist)
                _curPoint++;
        }

        private IEnumerator UpdatePathCoroutine()
        {
            while (true)
            {
                _seeker.StartPath(transform.position, target.position, 
                    OnPathComplete);
                var rate = Random.Range(minPathUpdateRate, maxPathUpdateRate);
                yield return new WaitForSeconds(rate);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (p.error)
                return;

            _path = p;
            _curPoint = 0;
        }
    }
}
