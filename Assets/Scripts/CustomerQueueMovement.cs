using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueMovement : MonoBehaviour
{

    private bool move = false;

    [SerializeField] private Animator anim;

    private Transform destination;
    private Transform origin;
    private float startTime;
    private float duration;

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            this.transform.position = new Vector3(LerpFunction.Lerp(origin.position.x, destination.position.x, startTime, duration), transform.position.y, transform.position.z);
        }
    }
    public void ResetCustomer()
    {
        this.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
        anim.Play("CustomerQueueBase");
        move = false;
    }   

    public void MoveAccept(Transform originMove, Transform destinationMove, float timeDuration)
    {
        origin = originMove;
        destination = destinationMove;
        duration = timeDuration;

        move = true;
        startTime = Time.time;
        anim.Play("CustomerQueueBase");
    }

    public void MoveDecline(Transform originMove, Transform destinationMove, float timeDuration)
    {
        origin = originMove;
        destination = destinationMove;
        duration = timeDuration;

        this.GetComponent<RectTransform>().transform.localScale = new Vector3(-1, 1, 1);
        move = true;
        startTime = Time.time;
        anim.Play("CustomerQueueBase");
    }
}
