using UnityEngine;

namespace Player
{
    public class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private float playerRadius = 0.7f;
        [SerializeField] private float playerHeight = 2f;
        
        //Check collision and change move direction 
        public bool IsColliding(ref Vector3 moveDirection, float moveDistance)
        {
            var canMove = !CheckCollision( moveDirection, moveDistance);

            if (!canMove)
            {
                var moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                if(moveDirectionX.x!=0 && !CheckCollision(moveDirectionX, moveDistance))
                {
                    canMove = true;
                    moveDirection = moveDirectionX;
                }
                if (!canMove)
                {
                    var moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    if(moveDirectionZ.z!=0 && !CheckCollision(moveDirectionZ, moveDistance))
                    {
                        canMove = true;
                        moveDirection = moveDirectionZ;
                    }
                }
            }
            return canMove;
        }
        
        
        
        private bool CheckCollision(Vector3 moveDirection, float moveDistance)
        {
            return Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirection, moveDistance);
        }
    }
}
