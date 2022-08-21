using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-50)]
    public class CharacterAttackInput : MonoBehaviour
    {
        public bool IsFiring => Input.GetButtonDown("Fire1");
    }
}
