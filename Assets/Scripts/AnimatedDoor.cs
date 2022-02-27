using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedDoor : MonoBehaviour
{
    //[SerializeField] private Transform body;
    public bool IsOpen = false;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private Vector3 SlideDirection = Vector3.up;
    [SerializeField] private float SlideAmount = 2.9f;
    private Vector3 StartPosition;
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        StartPosition = transform.position;
    }

    public void DoorControl()
    {
        if(IsOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoSlidingOpen());
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void CloseDoor()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(DoSlidingClose());
        }
    }
    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}
