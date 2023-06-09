using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OptionsUI : MonoBehaviour
    {
        public static OptionsUI Instance;
        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;

        [SerializeField] private Transform pressToReBindKeyTransform;
        
        [Space(10)]
        [SerializeField] private TextMeshProUGUI moveUpText;
        [SerializeField] private TextMeshProUGUI moveDownText;
        [SerializeField] private TextMeshProUGUI moveLeftText;
        [SerializeField] private TextMeshProUGUI moveRightText;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private TextMeshProUGUI interactAltText;
        [SerializeField] private TextMeshProUGUI pauseText;
        
        [Space(10)]
        [SerializeField] private Button moveUpButton;
        [SerializeField] private Button moveDownButton;
        [SerializeField] private Button moveLeftButton;
        [SerializeField] private Button moveRightButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button interactAltButton;
        [SerializeField] private Button pauseButton;


        private Action _onCloseButtonAction;
        private void Awake()
        {
            Instance = this;
            
            soundEffectsButton.onClick.AddListener((() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            } ));
            musicButton.onClick.AddListener((() =>
            {
               MusicManager.Instance.ChangeVolume(); 
               UpdateVisual();
            } ));
            closeButton.onClick.AddListener((() =>
            {
                Hide();
                _onCloseButtonAction();
            } ));
            
            moveUpButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.MoveUp);}));
            moveDownButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.MoveDown);}));
            moveLeftButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.MoveLeft);}));
            moveRightButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.MoveRight);}));
            interactButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.Interact);}));
            interactAltButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.InteractAlternate);}));
            pauseButton.onClick.AddListener((() => { RebindBinding(GameInput.Binding.Pause);}));
        }

        private void Start()
        {
            KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
            UpdateVisual();
            Hide();
            HidePressToRebindKey();
        }

        private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
        }

        private void UpdateVisual()
        {
            soundEffectsText.text = $"Sound Effects : {Mathf.Round(SoundManager.Instance.GetVolume() * 10f)}";
            musicText.text = $"Music : {Mathf.Round(MusicManager.Instance.GetVolume() * 10f)}";

            moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
            moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
            moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
            moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
            interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
            pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        }
        
        public void Show(Action onCloseButtonAction)
        {
            gameObject.SetActive(true);
            _onCloseButtonAction = onCloseButtonAction;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ShowPressToRebindKey()
        {
            pressToReBindKeyTransform.gameObject.SetActive(true);
        }
        
        private void HidePressToRebindKey()
        {
            pressToReBindKeyTransform.gameObject.SetActive(false);
        }

        private void RebindBinding(GameInput.Binding binding)
        {
            ShowPressToRebindKey();
            GameInput.Instance.RebindBinding(binding,() =>
            {
                HidePressToRebindKey();
                UpdateVisual();
            });
        }
    }
}
