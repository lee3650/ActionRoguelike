using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains hardcoded values for the correct blacklisted layers for a factions weapon
/// </summary>
public class WeaponLayerMask
{
    private static string[] playerLayers = new string[] { "Player", "PlayerAttack", "Dodge" };
    private static string[] enemyLayers = new string[] { "Enemy", "EnemyAttack", "Dodge" };

    public readonly static string DodgeLayer = "Dodge";
    public readonly static string DefaultPlayerLayer = "Player";

    public static LayerMask GetLayerMask(Factions f)
    {
        switch (f)
        {
            case Factions.Enemy:
                return ~LayerMask.GetMask(enemyLayers);
            case Factions.Player:
                return ~LayerMask.GetMask(playerLayers);
        }

        throw new System.Exception("Could not find layer mask for faction " + f);
    }
}
