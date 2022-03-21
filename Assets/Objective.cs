using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective
{
    public Sprite resource;
    public int amount;

    public Objective(Sprite _resource, int _amount)
    {
        resource = _resource;
        amount = _amount;
    }
}
