using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassForSpawningSpheresInGame : ClassForSpheresSpawn
{
    public int numberOfSpheresToSpawn;
    public float speedOfSpawnedSpheres;

    public void SpawnSpheres()
    {
        MethodForSettingColorsOnSpawnedSpheres(numberOfSpheresToSpawn);
    }

    public void Update()
    {
        
    }
}
