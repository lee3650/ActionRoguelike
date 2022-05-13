using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayShowPopup : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] PopupText Popup;

    public void Start()
    {
        StartCoroutine(ShowPopup());
    }

    IEnumerator ShowPopup()
    {
        yield return new WaitForSeconds(delay);
        Popup.Show("Testing!", 1f);
    }
}
