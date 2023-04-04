using UnityEngine;
using UnityEngine.UI;

namespace Counters.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;

        private IHasProgress _hasProgress;

        private void Start()
        {
            _hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (_hasProgress == null)
            {
                Debug.LogError($"Game Object {hasProgressGameObject.name} dont have IHasProgress!");
            }
            _hasProgress.OnProgressChanged +=HasProgress_ProgressChanged;
            barImage.fillAmount = 0f;
            Hide();
        }

        private void HasProgress_ProgressChanged(object sender, IHasProgress.OnProgressChangedArgs e)
        {
            barImage.fillAmount = e.ProgressNormalized;

            if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }


        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}