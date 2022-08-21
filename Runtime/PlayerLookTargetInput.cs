using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    public class PlayerLookTargetInput : CharacterLookTargetInput
    {
        private void Update()
        {
            lookTarget = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
