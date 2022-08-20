using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    public static event OnSwipeInput onSwipe;
    public static event OnSwipeInput onSwipeEnd;
    public delegate void OnSwipeInput(Vector2 direction);

    private bool isSwiping;
    private bool isMobile;
    private Vector3 tapPos = new Vector2();

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tapPos = Input.mousePosition;
            }

            if (!Input.GetMouseButton(0))
            {
                if (isSwiping)
                {
                    isSwiping = false;
                    if (tapPos != null)
                    {
                        onSwipeEnd?.Invoke(tapPos);
                    }
                }

                tapPos = Input.mousePosition;
                return;
            }

            if (!isSwiping)
            {
                isSwiping = true;
                //onSwipeStart?.Invoke(Input.mousePosition - tapPos);
            }

            onSwipe?.Invoke(Input.mousePosition - tapPos);
            tapPos = Input.mousePosition;
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    tapPos = (Vector3)Input.GetTouch(0).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (isSwiping)
                    {
                        isSwiping = false;
                        onSwipeEnd?.Invoke(tapPos);
                    }

                    tapPos = (Vector3)Input.GetTouch(0).position;
                    return;
                }

                if (!isSwiping)
                {
                    isSwiping = true;
                }

                onSwipe?.Invoke((Vector3)Input.GetTouch(0).position - tapPos);
                tapPos = (Vector3)Input.GetTouch(0).position;
            }
        }
    }
}