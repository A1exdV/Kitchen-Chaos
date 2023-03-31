using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;

    private bool _isWalking;
    private void Update()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDirection = new Vector3(inputVector.x,0f,inputVector.y);
        
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);

        _isWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime* rotateSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
