using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [FormerlySerializedAs("player")] [SerializeField] private PlayerMovement playerMovement;
    
        private const string IS_WALKING = "IsWalking";
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetBool(IS_WALKING,playerMovement.IsWalking());
        }
    }
}
