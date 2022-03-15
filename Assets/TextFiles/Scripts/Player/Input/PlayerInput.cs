using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Actually we can make this an interface
//and use it for testing... 
public abstract class PlayerInput : MonoBehaviour
{
    public abstract int GetTalentToActivate();
    public abstract Vector2 GetDirectionalInput();
    public abstract float GetDirectionToFace();
    public abstract bool Attack();
    public abstract bool Dodge();
    public abstract bool PickUpItems();
    public abstract int SelectionDelta();
    public abstract string GetTalentKey(int index);
}
