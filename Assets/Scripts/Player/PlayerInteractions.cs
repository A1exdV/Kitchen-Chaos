using System;
using Counter;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        public static PlayerInteractions Instance { get;private set; }
        
        [SerializeField] private LayerMask countersLayerMask;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private float interactDistance = 2f;

        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        
        public class OnSelectedCounterChangedEventArgs: EventArgs
        {
            public ClearCounter SelectedCounter;
        }
        
        private Vector3 _lastInteractDirection;
        private PlayerMovement _playerMovement;
        private ClearCounter _selectedCounter;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            if (Instance != null)
            {
                Debug.LogError("There is more than one Player instance!");
            }
            Instance = this;
        }

        private void Start()
        {
            gameInput.OnInteractAction +=GameInput_OnInteractAction;
        }

        private void GameInput_OnInteractAction(object sender, EventArgs e)
        {
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact();
            }
        }

        void Update()
        {
            HandleInteractions();
        }
        
        private void HandleInteractions()
        {
            var moveDirection = _playerMovement.GetMoveDirection();

            if (moveDirection != Vector3.zero)
            {
                _lastInteractDirection = moveDirection;
            }
            
            if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance,countersLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    if (clearCounter != _selectedCounter)
                        SetSelectedCounter(clearCounter);
                }
                else if(_selectedCounter!=null)
                {
                    SetSelectedCounter(null);
                }
            }
            else if(_selectedCounter!=null)
            {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(ClearCounter selectedCounter)
        {
            _selectedCounter = selectedCounter;
            
            OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs
            {
                SelectedCounter =_selectedCounter
            });
        }
    }
}
