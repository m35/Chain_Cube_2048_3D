using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;

    [SerializeField] private float forceValue;
    [SerializeField, Range(0.5f, 2.5f)] private float normCoef = 1.0f;

    [SerializeField] private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    private float borderDistance = 0f;
    private float offset = 0f;

    [SerializeField] private GameObject prefabCube;
    private GameObject cubeSpawn;
    private TrailRenderer cubeTail;
    private Rigidbody cube;

    private SwipeDetector swipeDetector;

    private void SpawnNewCube()
    {
        cubeSpawn = Instantiate(prefabCube, transform.position, transform.rotation);
        cube = cubeSpawn.GetComponent<Rigidbody>();
        cubeTail = cubeSpawn.GetComponent<TrailRenderer>();
        cubeTail.enabled = false;
    }

    private void Start()
    {
        borderDistance = Mathf.Abs(rightBorder.position.x - leftBorder.position.x);
        swipeDetector = GetComponent<SwipeDetector>();
        swipeDetector.onSwipe += OnSwipe;
        swipeDetector.onSwipeEnd += OnSwipeEnd;
    }

    private void FixedUpdate()
    {
        if (cube == null && timeBtwShots <= 0f)
        {
            SpawnNewCube();
            timeBtwShots = startTimeBtwShots;
        }
        else if (cube == null && timeBtwShots >= 0f)
        {
            timeBtwShots -= Time.deltaTime;
            return;
        }
    }

    private void OnSwipe(Vector2 delta)
    {
        if (cube == null)
        {
            return;
        }

        offset = borderDistance * normCoef * delta.x / Screen.width;
        
        cube.transform.position = new Vector3(cube.transform.position.x + offset, cube.transform.position.y, cube.transform.position.z);

        if (cube.transform.position.x > rightBorder.position.x)
        {
            cube.transform.position = rightBorder.transform.position;
        }
        else if (cube.transform.position.x < leftBorder.position.x)
        {
            cube.transform.position = leftBorder.transform.position;
        }

        transform.position = cube.transform.position;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        if (cube == null)
        {
            return;
        }
        cube.AddForce(cube.transform.forward * forceValue, ForceMode.Impulse);
        cubeSpawn = null;
        cube = null;
        cubeTail.enabled = true;
    }

    /*private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        if (_swipeDetector == null)
            return;
            
        _swipeDetector.onSwipe -= OnSwipe;
    }*/
}