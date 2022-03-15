using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : PlayerInput, Initializable
{
    [SerializeField] KeyCode[] TalentBinds = new KeyCode[ActiveTalentManager.MaxActiveTalents];
    private float[] LastTalentPresses;

    private float lastAttackPress;
    private float lastDodgePress;
    private float lastPositiveScroll;
    private float lastNegativeScroll;

    private float horInput;
    private float vertInput;

    const float frameThreshold = 1.9f; 

    public void Init()
    {
        LastTalentPresses = new float[TalentBinds.Length];
    }

    public override string GetTalentKey(int index)
    {
        return TalentBinds[index].ToString();
    }

    public static float GetRotationFromDirection(Vector2 dir)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x); 
    }

    public override int GetTalentToActivate()
    {
        for (int i = 0; i < LastTalentPresses.Length; i++)
        {
            if (Time.realtimeSinceStartup - LastTalentPresses[i] < frameThreshold * Time.fixedDeltaTime)
            {
                return i;
            }
        }
        return -1; 
    }

    public override int SelectionDelta()
    {
        if (Time.realtimeSinceStartup - lastPositiveScroll < Time.fixedDeltaTime)
        {
            return 1;
        }
        if (Time.realtimeSinceStartup - lastNegativeScroll < Time.fixedDeltaTime)
        {
            return -1;
        }
        return 0;
    }

    public override float GetDirectionToFace()
    {
        Vector2 delta = GetWorldMousePos() - (Vector2)transform.position; 
        return GetRotationFromDirection(delta);
    }

    public override Vector2 GetDirectionalInput()
    {
        return new Vector2(GetHorizontalInput(), GetVerticalInput()).normalized;
    }

    private float GetHorizontalInput()
    {
        return horInput;
    }

    private float GetVerticalInput()
    {
        return vertInput;
    }

    private Vector2 GetWorldMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }

    public override bool Attack()
    {
        return Time.realtimeSinceStartup - lastAttackPress < frameThreshold * Time.fixedDeltaTime;
        //return Input.GetMouseButton(0);
    }

    public override bool Dodge()
    {
        return Time.realtimeSinceStartup - lastDodgePress < frameThreshold * Time.fixedDeltaTime; 
    }

    public override bool PickUpItems()
    {
        return Input.GetKey(KeyCode.E);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastAttackPress = Time.realtimeSinceStartup;
        }
        if (Input.GetMouseButtonDown(1))
        {
            lastDodgePress = Time.realtimeSinceStartup;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            lastPositiveScroll = Time.realtimeSinceStartup;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            lastNegativeScroll = Time.realtimeSinceStartup; 
        }

        int left = Input.GetKey(KeyCode.A) ? -1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;

        horInput = left + right;

        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? -1 : 0;

        vertInput = up + down; 

        //vertInput = Input.GetAxisRaw("Vertical");
        //horInput = Input.GetAxisRaw("Horizontal");

        for (int i = 0; i < TalentBinds.Length; i++)
        {
            if (Input.GetKey(TalentBinds[i]))
            {
                print("got talent bind for " + TalentBinds[i]);
                LastTalentPresses[i] = Time.realtimeSinceStartup; 
            }
        }
    }
}
