using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains hardcoded values for the correct blacklisted layers for a factions weapon
/// </summary>
public class WeaponLayerMask
{
    public const string DodgeLayer = "Dodge";
    public const string DefaultPlayerLayer = "Player";
    public const string PlayerAttackLayer = "PlayerAttack";
    public const string PlayerInvLayer = "HitFrames";

    private static string[] playerLayers = new string[] { DefaultPlayerLayer, PlayerAttackLayer, DodgeLayer, PlayerInvLayer };
    private static string[] enemyLayers = new string[] { "Enemy", "EnemyAttack", DodgeLayer, PlayerInvLayer };

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
