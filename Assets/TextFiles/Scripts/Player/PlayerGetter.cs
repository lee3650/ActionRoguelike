using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class PlayerGetter : MonoBehaviour, LateInitializable
{
    [SerializeField] List<PlayerClass> ClassTypes = new List<PlayerClass>(Enum.GetValues(typeof(PlayerClass)).Length); 
    [SerializeField] GameObject[] Classes;
    [SerializeField] InjectionSet PlayerInjectionSet;
    public event Action<Transform> PlayerReady = delegate { };

    public static PlayerClass CurrentClass
    {
        get;
        set;
    }

    public void LateInit()
    {
        GameObject parent = Instantiate(Classes[ClassTypes.IndexOf(CurrentClass)], new Vector3(0, 0, 0), Quaternion.identity);
        //so, now we need to find the player... 
        Transform player = null;
        foreach (Transform t in parent.transform)
        {
            if (t.TryGetComponent<PlayerRoomSetter>(out PlayerRoomSetter s))
            {
                player = t;
                break;
            }
        }
        
        Debug.Assert(player != null, "The player was null after instantiation!");

        PlayerInjectionSet.InjectDependencies(player); 
        OrderedInit.PerformInitialization(player);

        PlayerReady(player);
    }
}
