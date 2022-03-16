using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HeartsManager : MonoBehaviour, LateInitializable
{
    [SerializeField] HealthManager HealthManager;
    [SerializeField] Transform HeartParent;
    [SerializeField] GameObject FullHeart;
    [SerializeField] GameObject HalfHeart;
    [SerializeField] int HealthPerHeart = 2;

    private List<GameObject> oldHearts = new List<GameObject>();

    public void LateInit()
    {
        HealthManager.HealthChanged += HealthChanged;
        HealthChanged();
    }

    private void HealthChanged()
    {
        float cur = HealthManager.GetCurHealth();
        int fullhearts = (int)(cur / HealthPerHeart);
        int halfs = (int)(cur % HealthPerHeart);

        for (int i = 0; i < oldHearts.Count; i++)
        {
            Destroy(oldHearts[i]);
        }

        oldHearts = new List<GameObject>();

        for (int i = 0; i < fullhearts; i++)
        {
            oldHearts.Add(Instantiate(FullHeart, HeartParent));
        }

        for (int i = 0; i < halfs; i++)
        {
            oldHearts.Add(Instantiate(HalfHeart, HeartParent));
        }
    }
}
