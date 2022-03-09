using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Entity
{
    void HandleEvent(GameEvent e);

    Transform GetTransform();
}
