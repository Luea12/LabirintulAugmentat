
using System;
using UnityEngine;

[Serializable]
public class Difficulty
{
    [Tooltip("Display name for the difficulty")]
    public string name;     // display name

    [Tooltip("Total number of coins to spawn")]
    public ushort totalcoins;  // total coins to spawn

    [Tooltip("Minimum number of coins to pickup")]
    public ushort treshold;    // minimum numberof coins to go to the next level

    [Tooltip("Initial maze size")]
    public ushort startSize;   // Starting maze size

    [Tooltip("Value to increase each level by")]
    public ushort levelUpSize; // newLevelSize = oldLevelSize + levelUpSize

    [Tooltip("Number of levels needed to win")]
    public ushort numberOfLevels;

    [Tooltip("Number of seconds if timed level")]
    public ushort numberOfSeconds;
}
