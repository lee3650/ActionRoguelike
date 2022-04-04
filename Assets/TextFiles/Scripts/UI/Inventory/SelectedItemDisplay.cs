using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedItemDisplay : MonoBehaviour
{
    [SerializeField] ItemActionButton ItemActionButtonPrefab;
    [SerializeField] Transform ItemGroup;
    [SerializeField] TextMeshProUGUI TitleText;
    [SerializeField] TextMeshProUGUI DescriptionText;

    private List<ItemActionButton> previousButtons = new List<ItemActionButton>();

    private Item currentItem;
    private ItemSupplier currentSupplier;

    private void DestroyPreviousButtons()
    {
        foreach (ItemActionButton iab in previousButtons)
        {
            Destroy(iab.gameObject);
        }

        previousButtons = new List<ItemActionButton>();
    }

    public void ShowSelectedItem(Item i, ItemSupplier source)
    {
        DestroyPreviousButtons();
        
        if (currentItem != null)
        {
            currentItem.ItemModified -= RefreshItem;
        }

        currentSupplier = source;

        currentItem = i;
        currentItem.ItemModified += RefreshItem; 

        foreach (ItemAction action in i.ValidActions)
        {
            ItemActionButton iab = Instantiate(ItemActionButtonPrefab, ItemGroup);
            iab.Initialize(action, this);
            previousButtons.Add(iab);
        }

        SetupDescriptions();
    }

    public void ActionClicked(ItemAction actionType)
    {
        currentSupplier.PerformActionOnItem(currentItem, actionType);
    }

    private void SetupDescriptions()
    {
        TitleText.text = currentItem.GetItemTitle();
        DescriptionText.text = currentItem.GetDescription();
    }

    private void RefreshItem()
    {
        ShowSelectedItem(currentItem, currentSupplier);
    }
}
