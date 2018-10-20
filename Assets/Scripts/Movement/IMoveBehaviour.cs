using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveBehaviour
{
    void Init(CharacterMovementData data);
    void SetHorizontal(float x);
    void Jump();

}
