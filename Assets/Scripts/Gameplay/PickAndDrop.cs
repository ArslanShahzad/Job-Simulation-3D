using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    [SerializeField] bool isPicking = true;
    public Transform[] Passangers;
    private bool isStoped = false;



    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MainBus"))
        {

            if(GameManager.instance.BusController.speed < 0.01f && !isStoped)
            {
                GameManager.instance.BusController.canControl = false;
                GameManager.instance.busManager.DoorAnim.SetBool("doorOpen", true);
                if (isPicking)
                {
                    GameManager.instance.checkPickPoints();
                    GameManager.instance.CommingPassanger = Passangers.Length;
                    StartCoroutine(CharacterMoving());
                }
                else
                {
                    GameManager.instance.checkDropPoints();
                    GameManager.instance.busManager.DropPassenger();
                }
                isStoped = true;
            }
        }
    }

    IEnumerator CharacterMoving()
    {
        for (int i = 0; i < Passangers.Length; i++)
        {
            Passangers[i].gameObject.GetComponent<passangerMover>().Path = GameManager.instance.busManager.PickPath;
            Passangers[i].gameObject.GetComponent<passangerMover>().MoveCharacter();

            yield return new WaitForSeconds(0.5f);
        }
    }
}
