using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ImagePulser : MonoBehaviour
{
    [SerializeField] XPManager XPManager;
    [SerializeField] float PulseSpeed;

    public void PulseTalent(List<Image> images)
    {
        StartCoroutine(Pulse(images));
    }

    public void StopAll()
    {
        //StopAllCoroutines();
    }

    IEnumerator Pulse(List<Image> images)
    {
        float timer = 0f;

        Prereq.Assert(images.Count > 0, "Count of points was 0!");

        print("pulsing!");

        Color original = images[0].color;

        float delay = Random.Range(0, 1f);

        yield return new WaitForSecondsRealtime(delay);

        while (!XPManager.HasPolicyInProgress())
        {
            timer += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);

            foreach (Image image in images)
            {
                image.color = Color.Lerp(original, Color.white, Mathf.Sin(timer * PulseSpeed));
            }
        }

        foreach (Image image in images)
        {
            image.color = original;
        }
    }
}
