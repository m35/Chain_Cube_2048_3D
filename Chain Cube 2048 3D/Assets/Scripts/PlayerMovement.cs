using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;

    [SerializeField] private float forceValue;
    [SerializeField, Range(0.5f, 2.5f)] private float normCoef = 1.0f;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    private float borderDistance = 0f;
    private float offset = 0f;

    [SerializeField] private GameObject prefabCube;
    private GameObject cubeSpawn;
    private TrailRenderer cubeTail;
    private Rigidbody cube;

    //private Animator animator;

    private LineRenderer line;
    
    private SwipeDetector swipeDetector;

    private int shoots;

    private void SpawnNewCube()
    {
        line.enabled = true;
        cubeSpawn = Instantiate(prefabCube, transform.position, transform.rotation);
        //animator.SetBool("isSpawned", true);
        cube = cubeSpawn.GetComponent<Rigidbody>();
        cubeTail = cubeSpawn.GetComponent<TrailRenderer>();
        cubeTail.enabled = false;
        shoots++;
    }

    private void Start()
    {
        InterstitialAds.myAds.LoadAd();
        shoots = 0;

        gameObject.SetActive(false);

        //animator = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
        line.startWidth = line.endWidth = 1f;
        line.SetPosition(1, Vector3.forward * 20f);
        line.enabled = true;

        borderDistance = Mathf.Abs(rightBorder.position.x - leftBorder.position.x);
        timeBtwShots = 0f;
        
        swipeDetector = GetComponent<SwipeDetector>();
        swipeDetector.onSwipe += OnSwipe;
        swipeDetector.onSwipeEnd += OnSwipeEnd;    
    }

    public void OffCube()
    {
        cubeSpawn.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (cube == null)
        {
            line.enabled = false;
        }

        if (shoots >= UnityEngine.Random.Range(15, 25))
        {
            InterstitialAds.myAds.ShowAd();
            shoots = 0;
        }

        if (cube == null && timeBtwShots <= 0f)
        {
            SpawnNewCube();
            timeBtwShots = startTimeBtwShots;
        }
        else if (cube == null && timeBtwShots > 0f)
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
}