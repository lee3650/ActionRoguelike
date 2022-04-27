using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class UnlockedClassManager : MonoBehaviour
{
    [SerializeField] List<PlayerClass> UnlockedClasses = new List<PlayerClass>();
    
    public event Action UpdatedUnlockedClasses = delegate { }; 

    public List<PlayerClass> GetUnlockedClasses()
    {
        return UnlockedClasses; 
    }
}
