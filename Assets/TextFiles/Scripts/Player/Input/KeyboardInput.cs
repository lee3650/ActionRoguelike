using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : PlayerInput
{
    public static float GetRotationFromDirection(Vector2 dir)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x); 
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
        return Input.GetMouseButtonDown(0);
    }

    public override bool Dodge()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
