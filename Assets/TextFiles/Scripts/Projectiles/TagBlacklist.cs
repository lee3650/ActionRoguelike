using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagBlacklist : MonoBehaviour
{
    [SerializeField] private List<string> BlacklistedTags = new List<string>() { "portal" };

    public bool IsTagBlacklisted(string tag)
    {
        return BlacklistedTags.Contains(tag);
    }
}
