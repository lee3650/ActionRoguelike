using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HeartsManager : MonoBehaviour, Initializable
{
    [SerializeField] PlayerGetter PlayerGetter;
    [SerializeField] Transform HeartParent;
    [SerializeField] GameObject FullHeart;
    [SerializeField] GameObject HalfHeart;
    [SerializeField] int HealthPerHeart = 2;

    HealthManager HealthManager;
    private List<GameObject> oldHearts = new List<GameObject>();

    public void Init()
    {
        PlayerGetter.PlayerReady += PlayerReady;
    }

    private void PlayerReady(Transform obj)
    {
        HealthManager = obj.GetComponent<HealthManager>();
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
