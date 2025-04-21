using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
     static event Func<Transform> SeatOccuption;
     static event Action busPassanger;


    void Start()
    {
        SeatOccuption += () => GameManager.instance.busManager.CheckAvailbleSeat();
        busPassanger += () => GameManager.instance.BusReadyToMove();
    }

    

    public static Transform GetSeat() => SeatOccuption?.Invoke();
    public static void BusReadyToMove() => busPassanger?.Invoke();


}
