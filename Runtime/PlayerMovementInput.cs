using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    public class PlayerMovementInput : CharacterMovementInput
    {
        private void Update()
        {
            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");
        }
    }
}
