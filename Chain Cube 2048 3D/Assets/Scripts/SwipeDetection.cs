using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);

    private Vector3 tapPos;
    private Vector3 swipeDelta;

    private bool isSwiping;
    private bool isMobile;

    public float delta;

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    /*
    private void FixedUpdate()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                tapPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CheckSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwiping = true;
                    tapPos = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    CheckSwipe();
                }
            }
        }
    }
    */

    private void FixedUpdate()
    {
        if (!Input.GetMouseButton(0))
        {
            if (isSwiping)
            {
                isSwiping = false;
                swipeDelta = tapPos;
                SwipeEvent(swipeDelta);
            }

            tapPos = Input.mousePosition;
            return;
        }

        if (!isSwiping)
        {
            isSwiping = true;
            swipeDelta = Input.mousePosition - tapPos;
            SwipeEvent(swipeDelta);
        }

        swipeDelta = Input.mousePosition - tapPos;
        SwipeEvent(swipeDelta);
        tapPos = Input.mousePosition;
    }

    /*
    private void CheckSwipe()
    {
        swipeDelta = Vector2.zero;

        //if (isSwiping)
        //{
            if (!isMobile && Input.GetMouseButtonUp(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - tapPos;
            }
            else if (Input.touchCount > 0)
            {
                swipeDelta = Input.GetTouch(0).position - tapPos;
            }
        //}

        if (SwipeEvent != null)
        {
            Debug.Log(222222);
            SwipeEvent(swipeDelta);
        }

        //ResetSwipe();
    }

    private void ResetSwipe()
    {
        isSwiping = false;
        tapPos = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
    */
}