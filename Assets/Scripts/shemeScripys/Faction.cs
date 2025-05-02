using System;
using UnityEngine;

[Serializable]
public class Faction
{
    public enum FactionType { Player, Ally, Enemy, Neutral }
    public FactionType factionType;

    [Tooltip("Layers that this faction considers enemies")]
    public LayerMask enemyMask;

    public bool IsEnemy(Faction other)
    {
        if (other == null) return false;
        return (enemyMask & (1 << (int)other.factionType)) != 0;
    }

    public bool IsEnemy(LayerMask otherLayer)
    {
        return (enemyMask & otherLayer) != 0;
    }
}