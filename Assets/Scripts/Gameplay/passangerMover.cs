using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passangerMover : MonoBehaviour
{
    public Transform[] Path;
    [SerializeField] int index = 0;
    private bool _canMove = false;
    [SerializeField] float speed = 1f;
    public bool isDrop = false;
    public void MoveCharacter()
    {
        _canMove = true;
        GetComponent<Animator>().SetBool("isWalking", true);
    }

    private void Update()
    {
        if (_canMove)
        {
            if (index < Path.Length)
            {
                transform.LookAt(Path[index]);
                transform.rotation = Quaternion.Euler(0,transform.eulerAngles.y,0);

                Transform targetWaypoint = Path[index];
                Vector3 direction = targetWaypoint.position - transform.position;
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                // transform.position = Vector3.Lerp(transform.position, pickPath[index].position, 1f * Time.deltaTime);
                float distance = Vector3.Distance(transform.position, Path[index].position);
                if (distance < 0.1f)
                {
                    index++;
                }
            }
            else
            {
                _canMove = false;
                index = 0;
                if (!isDrop)
                {
                    GetComponent<Animator>().SetBool("isWalking", false);
                    Transform seatPosition = GameEvents.GetSeat();
                    transform.position = seatPosition.position;
                    transform.rotation = seatPosition.rotation;
                    transform.SetParent(seatPosition);
                }
                else
                {
                    Destroy(this.gameObject);
                }
                GameEvents.BusReadyToMove();
            }
        }
    }

    //void UpdatePath()
    //{
    //    if (index < Path.Length) 
    //    {
    //        index++;
    //        MoveCharacter();
    //    }
    //}
}
