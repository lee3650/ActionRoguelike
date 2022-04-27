using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraulicPress : MonoBehaviour, Initializable
{
    [SerializeField] float offset;
    [SerializeField] float anticipation; 
    [SerializeField] float attackLength;
    [SerializeField] float recovery;
    [Tooltip("How long after an attack completes to start another attack")]
    [SerializeField] float delay;
    [SerializeField] RoomChild RoomChild;
    [SerializeField] Transform PressGraphic;
    [SerializeField] float normalHeight, anticipateHeight;
    [SerializeField] Collider2D col;
    [SerializeField] bool ContinuePressing; 

    public void Init()
    {
        RoomChild.RoomEntered += RoomEntered;
    }

    private void RoomEntered()
    {
        print("parent set!");
        StartCoroutine(Press());
    }

    IEnumerator Press()
    {
        yield return new WaitForSeconds(offset);

        float timer = 0f;

        float[] stateLength = new float[4]
        {
            anticipation, attackLength, recovery, delay
        };

        System.Action<float>[] actions = new System.Action<float>[4]
        {
            Anticipate, Attack, Recover, Delay
        };

        System.Action[] exits = new System.Action[]
        {
            ExitAnticipate, ExitAttack, ExitRecovery, ExitDelay
        };

        int state = 0; 

        while (RoomChild.RoomActive() || ContinuePressing)
        {
            yield return null;
            timer += Time.deltaTime;

            if (timer > stateLength[state])
            {
                exits[state]();
                state = (state + 1) % 4;
                timer = 0f; 
            }

            //pass in normalized time
            actions[state](timer/stateLength[state]);
        }

        col.enabled = false; 

        print("finished pressing!");
    }

    private void ExitAnticipate()
    {

    }

    private void ExitAttack()
    {
        col.enabled = false;
        PressGraphic.localPosition = new Vector3(0, 0, 0);
    }

    private void ExitRecovery()
    {
        PressGraphic.localPosition = new Vector3(0, normalHeight, 0);
    }

    private void ExitDelay()
    {

    }

    private void Anticipate(float ntime)
    {
        PressGraphic.localPosition = new Vector3(0, Mathf.Lerp(normalHeight, anticipateHeight, ntime), 0);
    }

    private void Attack(float ntime)
    {
        PressGraphic.localPosition = new Vector3(0, Mathf.Lerp(anticipateHeight, 0, ntime), 0);

        if (ntime > 0.8f)
        {
            col.enabled = true; 
        }
    }

    private void Recover(float ntime)
    {
        PressGraphic.localPosition = new Vector3(0, Mathf.Lerp(0, normalHeight, ntime), 0);
    }

    private void Delay(float ntime)
    {

    }
}
