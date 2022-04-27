using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvulnerable : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float pulseAmplitude = 0.25f;
    [SerializeField] float pulseSpeed = 1f;
    [SerializeField] float intercept = 0.75f; 
    private bool invulnerable = false;
    private bool _flash = false; 

    /// <summary>
    /// Returns a or b, depending on which c is closest to. 
    /// </summary>
    private float step(float a, float b, float c)
    {
        if (c < a)
        {
            return a;
        }
        if (c > b)
        {
            return b; 
        }
        if (Mathf.Abs(a - c) > Mathf.Abs(b - c))
        {
            return b;
        }
        return a; 
    }

    private void FixedUpdate()
    {
        if (flash)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, step(0.5f, 1f, pulseAmplitude * Mathf.Sin(Time.realtimeSinceStartup * Mathf.PI * 0.5f * pulseSpeed) + (intercept)));
        }
    }

    private bool flash
    {
        get
        {
            return _flash;
        }
        set
        {
            _flash = value; 
            if (!_flash)
            {
                resetAlpha();
            }
        }
    }

    public void SetInvulnerable(bool _flash)
    {
        invulnerable = true;
        ChangeLayer(true);
        flash = _flash; 
    }

    public void ResetInvulnerable()
    {
        StopAllCoroutines();
        invulnerable = false;
        ChangeLayer(false);
        flash = false;
    }

    public void StartTimedInvulnerability(float length, bool _flash)
    {
        if (invulnerable)
        {
            return;
        }

        flash = _flash;

        StopAllCoroutines();
        StartCoroutine(TimedInvulnerable(length)); 
    }

    IEnumerator TimedInvulnerable(float len)
    {
        invulnerable = true;
        ChangeLayer(true);
        yield return new WaitForSeconds(len);
        invulnerable = false;
        ChangeLayer(false);
        flash = false; 
    }

    private void ChangeLayer(bool inv)
    {
        if (inv)
        {
            gameObject.layer = LayerMask.NameToLayer(WeaponLayerMask.PlayerInvLayer);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(WeaponLayerMask.DefaultPlayerLayer);
        }
    }

    private void resetAlpha()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }
}
