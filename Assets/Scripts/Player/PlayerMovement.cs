using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameInput gameInput;

        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float rotateSpeed = 10f;
        

        private bool _isWalking;
        private PlayerPhysics _playerPhysics;


        private void Awake()
        {
            _playerPhysics = GetComponent<PlayerPhysics>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var moveDirection = GetMoveDirection();
            
            var moveDistance = moveSpeed * Time.deltaTime;
            
            if (_playerPhysics.IsColliding(ref moveDirection,moveDistance))
            {
                transform.position += moveDirection * moveDistance;
            }

            _isWalking = moveDirection != Vector3.zero;

            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }

        
        
        public bool IsWalking()
        {
            return _isWalking;
        }

        public Vector3 GetMoveDirection()
        {
            var inputVector = gameInput.GetMovementVectorNormalized();
            return new Vector3(inputVector.x, 0f, inputVector.y);
        }
        
    }
}