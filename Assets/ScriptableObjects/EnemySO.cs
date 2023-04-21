using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public Sprite sprite;
    public float maxHP;
    public float moveSpeed;
    public float damage;
}
