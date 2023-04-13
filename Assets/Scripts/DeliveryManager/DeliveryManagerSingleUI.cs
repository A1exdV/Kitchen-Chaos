using System;
using KitchenObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if(child == iconContainer) continue;
            Destroy(child.gameObject);
        }

        foreach (var kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            var iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            var ImageTemplate = iconTransform.GetComponent<Image>();
            ImageTemplate.sprite = kitchenObjectSO.sprite;
            ImageTemplate.enabled = true;
        }
    }
}