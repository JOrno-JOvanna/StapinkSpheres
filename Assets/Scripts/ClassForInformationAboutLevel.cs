using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInformationAboutLevels", menuName = "Create new level asset / NewInformationAboutLevels")]
public class ClassForInformationAboutLevel : ScriptableObject
{
    [Serializable]
    public struct LevelsStruct
    {
        public int levelNumber;
        public int goalPoints;
        public int numberOfSpheresToSpawn;
        public float speedOfSpawnedSpheres;
        public bool verticalMoving;
    }

    public List<LevelsStruct> LevelsStructs;
}
