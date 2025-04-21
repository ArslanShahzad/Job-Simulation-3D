using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{

    public int TotalPickPoints;
    private int _pickPointCounter;

    public int TotalDropPoints;
    private int _dropPointCounter;

    public bool isAllPickPoints()
    {
        _pickPointCounter++;
        if(_pickPointCounter == TotalPickPoints)
        {
            return true;
        }
        return false;
    }

    public bool isAllDropPoints()
    {
        _dropPointCounter++;
        if (_dropPointCounter == TotalDropPoints)
        {
            return true;
        }
        return false;
    }
}
