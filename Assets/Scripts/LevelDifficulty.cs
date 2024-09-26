using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelDifficulty : ScriptableObject
{
    public int MaxNumberOfSpheres;
    public int MinNumberOfSpheres;
    public float DistanceBetweenSpheres;
    public float SpeedOfSpheres;
    public int ScoreGoal;
}
