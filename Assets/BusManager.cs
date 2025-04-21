using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BusManager : MonoBehaviour
{
    public Transform[] PickPath;
    public Transform[] DropPath;
    public Animator DoorAnim;

    [SerializeField] internal List<OccupieSeat> seats;

    private void Start()
    {
    }

    public Transform CheckAvailbleSeat()
    {
        OccupieSeat seat = seats.Where(obj => obj.isOccupie == false).FirstOrDefault();
        seat.isOccupie = true;
        int index = seats.IndexOf(seat);
        return seat.seatPosition;
    }

  //  int passengerDropCounter;
    public void DropPassenger()
    {
       GameManager.instance.CommingPassanger = seats.Count(obj => obj.isOccupie == true);
    //    passengerDropCounter = Random.Range(1, GameManager.instance.CommingPassanger);
    //    GameManager.instance.CommingPassanger = passengerDropCounter;
        foreach (var item in seats)
        {
            //if (passengerDropCounter > 0)
            //{
                if (item.isOccupie)
                {
                 //   passengerDropCounter--;
                    item.isOccupie = false;
                    print("DropPassenger");
                    item.isOccupie = false;
                    Transform passanger = item.seatPosition.GetChild(0);
                    passanger.gameObject.GetComponent<passangerMover>().Path = GameManager.instance.busManager.DropPath;
                    passanger.parent = null;
                    passanger.gameObject.GetComponent<passangerMover>().isDrop = true;
                    passanger.gameObject.GetComponent<passangerMover>().MoveCharacter();
                }
           // }
        }


    }

    [System.Serializable]
    internal class OccupieSeat
    {
        public Transform seatPosition;
        public bool isOccupie;
    }
}
