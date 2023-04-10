using System;
using KitchenObjects;
using Player;
using UnityEngine;

namespace Counters
{
    public class StoveCounter : BaseCounter, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State State;
        }
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned,
        }

        [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
        [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


        private State _state;
        private float _fryingTimer;
        private FryingRecipeSO _fryingRecipeSO;
        
        private float _burningTimer;
        private BurningRecipeSO _burningRecipeSO;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                switch (_state)
                {
                    case State.Idle:
                        break;
                    case State.Frying:
                        _fryingTimer += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs()
                        {
                            ProgressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
                        });
                        
                        if (_fryingTimer > _fryingRecipeSO.fryingTimerMax)
                        {
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);

                            _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                            _burningTimer = 0f;
                            _state = State.Fried;
                            
                            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                            {
                                State = _state
                            });
                            
                            
                        }
                        break;
                    case State.Fried:
                        _burningTimer += Time.deltaTime;
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs()
                        {
                            ProgressNormalized = _burningTimer / _burningRecipeSO.burningTimerMax
                        });
                        if (_burningTimer > _burningRecipeSO.burningTimerMax)
                        {
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(_burningRecipeSO.output, this);

                            _state = State.Burned;
                            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                            {
                                State = _state
                            });
                            
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs()
                            {
                                ProgressNormalized = 1f
                            });
                            
                        }
                        break;
                    case State.Burned:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override void Interact(PlayerInteractions player)
        {
            if (!HasKitchenObject())
            {
                //There is no KitchenObjects here
                if (player.HasKitchenObject())
                {
                    //Player is carrying something
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenCounterParent(this);

                        _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        _state = State.Frying;
                        _fryingTimer = 0f;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                        {
                            State = _state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs()
                        {
                            ProgressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
                        });
                    }
                }
                else
                {
                    //Player not carrying anything
                }
            }
            else
            {
                //There is a KitchenObjects here
                if (player.HasKitchenObject())
                {
                    //Player is carrying something
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {//Player is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                            
                            _state = State.Idle;
                            
                            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                            {
                                State = _state
                            });
                    
                            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs()
                            {
                                ProgressNormalized = 0f
                            });
                        }
                    }
                }
                else
                {
                    //Player not carrying anything
                    GetKitchenObject().SetKitchenCounterParent(player);

                    _state = State.Idle;
                    OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                    {
                        State = _state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedArgs()
                    {
                        ProgressNormalized = 0f
                    });
                }
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

            return fryingRecipeSO != null;
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
            if (fryingRecipeSO != null)
            {
                return fryingRecipeSO.output;
            }
            else
            {
                return null;
            }
        }

        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var fryingRecipeSO in fryingRecipeSOArray)
            {
                if (fryingRecipeSO.input == inputKitchenObjectSO)
                {
                    return fryingRecipeSO;
                }
            }

            return null;
        }
        
        private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var burningRecipeSO in burningRecipeSOArray)
            {
                if (burningRecipeSO.input == inputKitchenObjectSO)
                {
                    return burningRecipeSO;
                }
            }

            return null;
        }
    }
}