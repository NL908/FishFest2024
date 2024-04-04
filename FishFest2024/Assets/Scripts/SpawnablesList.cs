using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawnables", menuName = "ScriptableObjects/SpawnablesList", order = 1)]
public class SpawnablesList : ScriptableObject
{
    [SerializeField] public CollidableEntity[] spawnables;
}
