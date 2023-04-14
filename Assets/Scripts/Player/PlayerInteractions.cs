using System;
using Counters;
using UnityEngine;


namespace Player
{
    public class PlayerInteractions : MonoBehaviour, IKitchenObjectParent
    {
        public static PlayerInteractions Instance { get;private set; }
        
        [SerializeField] private LayerMask countersLayerMask;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private float interactDistance = 2f;
        [SerializeField]private Transform kitchenObjectHoldPoint;
        
        private KitchenObject _kitchenObject;

        public event EventHandler OnPickedSomething;
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        
        public class OnSelectedCounterChangedEventArgs: EventArgs
        {
            public BaseCounter SelectedCounter;
        }
        
        private Vector3 _lastInteractDirection;
        private PlayerMovement _playerMovement;
        private BaseCounter _selectedCounter;

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
            gameInput.OnInteractAlternateAction +=GameInput_OnInteractAlternateAction;
        }

        private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
        {
            if (!KitchenGameManager.Instance.IsGamePlaying())
            {
               return; 
            }
            
            if (_selectedCounter != null)
            {
                _selectedCounter.InteractAlternate(this);
            }
        }

        private void GameInput_OnInteractAction(object sender, EventArgs e)
        {
            if (!KitchenGameManager.Instance.IsGamePlaying())
            {
                return; 
            }
            if (_selectedCounter != null)
            {
                _selectedCounter.Interact(this);
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
                if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (baseCounter != _selectedCounter)
                        SetSelectedCounter(baseCounter);
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

        private void SetSelectedCounter(BaseCounter selectedCounter)
        {
            _selectedCounter = selectedCounter;
            
            OnSelectedCounterChanged?.Invoke(this,new OnSelectedCounterChangedEventArgs
            {
                SelectedCounter =_selectedCounter
            });
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return kitchenObjectHoldPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;

            if (kitchenObject != null)
            {
                OnPickedSomething?.Invoke(this,EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}
