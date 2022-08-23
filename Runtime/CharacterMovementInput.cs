using Unity.Netcode;
using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-50)]
    public class CharacterMovementInput : NetworkBehaviour
    {
        /// <summary>
        /// Automatically normalized movement direction.
        /// </summary>
        public Vector2 NormalizedDirection
        {
            get => direction.normalized;
            set => direction = value.normalized;
        }

        protected Vector2 direction;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                Destroy(this);
        }
    }
}
