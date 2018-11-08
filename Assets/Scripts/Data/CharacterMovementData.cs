using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Caracter Data", menuName = "Caracter/MovementData")]
public class CharacterMovementData : ScriptableObject
{
    public float speed;
    public float jumpSpeed;
    public float fallSpeed;
    public float fallMultiplier;
}