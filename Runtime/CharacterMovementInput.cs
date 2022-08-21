using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-50)]
    public class CharacterMovementInput : MonoBehaviour
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
    }
}
