using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionOnAction : MonoBehaviour, Initializable
{
    [SerializeField] WeaponManager WeaponManager;
    [SerializeField] WiggleOverTime Wiggle;

    private Vector2 initialPos;
    private bool lastActionFinishedFrame = true;

    public void Init()
    {
        initialPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (WeaponManager.ActionFinished() != lastActionFinishedFrame)
        {
            Wiggle.ResetWiggle();
        }

        lastActionFinishedFrame = WeaponManager.ActionFinished();
    }
}
