using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : PlayerInput
{
    private float lastAttackPress;
    private float lastDodgePress;
    private float lastPositiveScroll;
    private float lastNegativeScroll;

    public static float GetRotationFromDirection(Vector2 dir)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x); 
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
        return Input.GetAxisRaw("Horizontal");
    }

    private float GetVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    private Vector2 GetWorldMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }

    public override bool Attack()
    {
        return Time.realtimeSinceStartup - lastAttackPress < Time.fixedDeltaTime;
    }

    public override bool Dodge()
    {
        return Time.realtimeSinceStartup - lastDodgePress < Time.fixedDeltaTime; 
    }

    public override bool PickUpItems()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastAttackPress = Time.realtimeSinceStartup;
        }
        if (Input.GetKeyDown(KeyCode.Space))
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
    }
}
