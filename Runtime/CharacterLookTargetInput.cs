using TeaGames.Unity.GameplayFramework.Runtime;
using TeaGames.Unity.Utils.Runtime;
using UnityEngine;

namespace TeaGames.Unity.TopDownEngine.Runtime
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-50)]
    public class CharacterLookTargetInput : MonoBehaviour
    {
        public Vector3 LookTarget
        {
            get => lookTarget;
            set => lookTarget = value;
        }

        protected Vector3 lookTarget;
        protected Camera cam;

        private void Awake()
        {
            cam = SceneServices.Get<PlayerCameraManager>().Current;
        }
    }
}
