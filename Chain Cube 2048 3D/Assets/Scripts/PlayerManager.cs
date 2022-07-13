using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float forceValue;
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //SwipeDetection.SwipeEvent += OnSwipe;
    }

    private void FixedUpdate()
    {

        SwipeDetection.SwipeEvent += OnSwipe;
        if (transform.position.x > rightBorder.position.x)
            transform.position = rightBorder.transform.position;
        else if (transform.position.x < leftBorder.position.x)
            transform.position = leftBorder.transform.position;
    }

    private void OnSwipe(Vector2 dir)
    {
        if (Mathf.Abs(dir.x - Mathf.Epsilon) <= 0)
            return;

        var borderDistance = Mathf.Abs(rightBorder.position.x - leftBorder.position.x);
        var offset = borderDistance * 0.001f * dir.x / Screen.width;
        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
    }

    private void Push()
    {
        rb.velocity = Vector3.forward * forceValue;
    }
}
