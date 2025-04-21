using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Current Bus Data
    public GameObject CurrentBus;
    public BusManager busManager;
    public RCC_CarControllerV3 BusController;
    public int CommingPassanger = 0;
    private int _cPassangerCounter = 0;

    public LevelHandler levelHandler;
    public GameObject[] Levels;

    private bool isAllPickPoints;
    private bool isAllDropPoints;

    // Curent Level Handler
    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        if(CurrentBus.TryGetComponent<BusManager>(out BusManager controller))
        {
            busManager = controller;
        }
        if (CurrentBus.TryGetComponent<RCC_CarControllerV3>(out RCC_CarControllerV3 busController))
        {
            BusController = busController;
        }

        levelHandler = Levels[0].GetComponent<LevelHandler>();

    }
    public void checkPickPoints()
    {
        isAllPickPoints = levelHandler.isAllPickPoints();
        if (isAllPickPoints)
        {
            print("Pick All Passenger");
            LevelComplete();
        }
    }

    public void checkDropPoints()
    {
        isAllDropPoints = levelHandler.isAllDropPoints();
        if (isAllDropPoints)
        {
            print("Pick All Passenger");
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        if(isAllDropPoints && isAllDropPoints)
        {
            print("LevelCompleted");
        }
    }

    public void BusReadyToMove()
    {
        _cPassangerCounter++;

        if(_cPassangerCounter == CommingPassanger)
        {
            _cPassangerCounter = 0;
            busManager.DoorAnim.SetBool("doorOpen", false);
            BusController.canControl = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
