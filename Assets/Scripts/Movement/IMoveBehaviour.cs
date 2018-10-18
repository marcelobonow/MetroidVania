using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveBehaviour
{
    /// <summary>
    /// Seta a velocidade de acordo com o input
    /// </summary>
    /// <param name="x">input horziontal</param>
    /// <param name="y">input vertical</param>
    void SetHorizontal(float x);
    void SetVertical(float y);
    void Jump();

    void Init(float speed, float jumpForce);
}
