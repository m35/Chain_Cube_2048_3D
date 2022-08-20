using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerMovement : MonoBehaviour
{
    public static bool isActiveForRestart = true;

    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;

    [SerializeField] private float forceValue;
    [SerializeField, Range(0.5f, 1.5f)] private float normCoef = 1.0f;

    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    private float borderDistance = 0f;
    private float offset = 0f;

    [SerializeField] private GameObject prefabCube;
    private GameObject cubeSpawn;
    private TrailRenderer cubeTail;
    private Rigidbody cube;

    private LineRenderer line;

    private int shoots;

    private void SpawnNewCube()
    {
        shoots++;
        /*
        if (shoots >= UnityEngine.Random.Range(15, 25))
        {
            Time.timeScale = 0f;
            InterstitialAds.myAds.ShowAd();
            shoots = 0;
        }
        else
        {
            */
            line.enabled = true;
            cubeSpawn = Instantiate(prefabCube, transform.position, transform.rotation);
            cube = cubeSpawn.GetComponent<Rigidbody>();
            cube.isKinematic = true;
            cubeTail = cubeSpawn.GetComponent<TrailRenderer>();
            cubeTail.enabled = false;
        //}
    }

    private void Start()
    {
        //Time.timeScale = 0.1f;

        //InterstitialAds.myAds.LoadAd();
        shoots = 0;

        line = GetComponent<LineRenderer>();
        line.startWidth = line.endWidth = 1f;
        line.SetPosition(1, Vector3.forward * 20f);
        line.enabled = true;

        borderDistance = Mathf.Abs(rightBorder.position.x - leftBorder.position.x);
        timeBtwShots = 0f;

        SwipeDetector.onSwipe += OnSwipe;
        SwipeDetector.onSwipeEnd += OnSwipeEnd;
    }

    public void OffCube()
    {
        if (cubeSpawn != null)
        {
            cubeSpawn.SetActive(false);
        }
    }

    private void Update()
    {
        if (cube == null)
        {
            line.enabled = false;
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
        if (Time.timeScale > 0f)
        {
            if (cube == null)
            {
                return;
            }

            Cube cb = cube.gameObject.GetComponent<Cube>();
            cb.SetUnActiveIsSpawned();

            cube.isKinematic = false;
            cube.AddForce(cube.transform.forward * forceValue, ForceMode.Impulse);

            cubeSpawn = null;
            cube = null;
        }
    }
}