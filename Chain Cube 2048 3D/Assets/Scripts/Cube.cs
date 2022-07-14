using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    private TrailRenderer cubeTail;
    private Cube col;
    private GameObject[] allCubes;

    [SerializeField] private MeshRenderer render;
    [SerializeField] private TextMeshPro[] posRanks;
    [SerializeField] private List<MatSettings> install = new List<MatSettings>();

    private ScoreManager score;

    [SerializeField] private GameObject deadLine;
    private bool isDeadLine;

    [SerializeField] private GameObject prefabCube;
    [SerializeField] private GameObject floatingRank;
    [SerializeField] private int rank;

    [SerializeField] private float forceValue;
    [SerializeField] private float offset;

    [SerializeField] private GameObject boom;
    [SerializeField] private GameObject confetti;
    [SerializeField] private float offsetB;
    [SerializeField] private float offsetC;

    private Animator animator;

    private SwipeDetector swipeDetector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isSpawned", true);
    }

    private void Start()
    {
        int rand = Random.Range(0, 100);
        if (rand < 40)
        {
            rank = 2;
        }
        else if (rand < 60)
        {
            rank = 4;
        }
        else if (rand < 80)
        {
            rank = 8;
        }
        else if (rand < 90)
        {
            rank = 16;
        }
        else if (rand < 96)
        {
            rank = 32;
        }
        else if (rand <= 99)
        {
            rank = 64;
        }
        else if (rand == 100)
        {
            rank = 128;
        }
        SetRank(rank);

        swipeDetector = GetComponent<SwipeDetector>();
        //swipeDetector.onSwipeEnd += OnSwipeEnd;
        score = FindObjectOfType<ScoreManager>();

        rb = GetComponent<Rigidbody>();
        cubeTail = GetComponent<TrailRenderer>();
    }

    //private void OnSwipeEnd(Vector2 delta)
    private void OnSwipeEnd()
    {
        if (animator == null)
        {
            return;
        }
        //animator.SetBool("isSpawned", false);
        transform.localScale = new Vector3(2f, 2f, 2f);
        transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
        //swipeDetector.onSwipeEnd -= OnSwipeEnd;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cubeTail != null)
        {
            if (cubeTail.enabled)
            {
                cubeTail.enabled = false;
                OnSwipeEnd();
            }
        }

        if (cubeTail != null && collision.gameObject.tag != "Ground")
        {
            isDeadLine = true;
        }

        if (collision.gameObject.tag == "Cube")
        {
            col = collision.gameObject.GetComponent<Cube>();
            if (rank == col.rank)
            {
                Vector3 newCubeVector = (col.transform.position + transform.position) / 2f;
                Smash(col);
                transform.position = newCubeVector;

                Instantiate(floatingRank, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f), Quaternion.identity);
                floatingRank.GetComponentInChildren<FloatingRank>().rank = rank;

                score.AddRank(rank);

                GameObject closestCube = ClosestCube();
                if (closestCube != null)
                {
                    rb.velocity = closestCube.transform.position - transform.position;
                    rb.velocity = new Vector3(rb.velocity.x, forceValue, rb.velocity.z) / offset;
                    if (rb.velocity.x > forceValue)
                    {
                        rb.velocity = new Vector3(forceValue, rb.velocity.y, rb.velocity.z);
                    }
                    if (rb.velocity.y > forceValue)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, forceValue, rb.velocity.z);
                    }
                    if (rb.velocity.z > forceValue)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forceValue);
                    }
                }
                else
                {
                    rb.velocity += Vector3.up * forceValue;
                    if (rb.velocity.y > forceValue)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, forceValue, rb.velocity.z);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDeadLine)
        {
            if(transform.position.z <= deadLine.transform.position.z)
            {
                Debug.Log("YOU LOSE!");
            }
        }
    }

    private GameObject ClosestCube()
    {
        GameObject closest = null;
        allCubes = GameObject.FindGameObjectsWithTag("Cube");

        float dist = 100f;
        
        foreach (GameObject tmp in allCubes)
        {
            Cube tmpCube = tmp.gameObject.GetComponent<Cube>();
            if (tmpCube.rank == rank && tmp.transform.position != transform.position && tmp.transform.position.z > 3.5f)
            {
                Vector3 diff = tmp.transform.position - transform.position;
                float curDist = diff.sqrMagnitude;
                if (curDist < dist)
                {
                    closest = tmp;
                    dist = curDist;
                }
            }
        }

        return closest;
    }

    private void Smash(Cube col)
    {
        rank *= 2;
        SetRank(rank);
        Destroy(col.gameObject);
        if (boom != null)
        {
            Instantiate(boom, new Vector3(transform.position.x, transform.position.y + offsetB, transform.position.z), Quaternion.identity);
        }
        if (confetti != null)
        {
            GameObject tmp = Instantiate(confetti, new Vector3(transform.position.x, transform.position.y + offsetC, transform.position.z), Quaternion.identity);
            ParticleSystem nowConfetti = tmp.GetComponent<ParticleSystem>();
            nowConfetti.startColor = render.material.color;
        }
    }

    public void SetRank(int rank)
    {
        foreach (TextMeshPro text in posRanks)
        {
            text.text = rank.ToString();

            MatSettings settings = install.Find(tmp => tmp.rank == rank);

            if (settings == null)
            {
                render.material = default;
            }
            else
            {
                render.material = settings.material;
            }
        }
    }

    [System.Serializable]
    public class MatSettings
    {
        public int rank;
        public Material material;
    }
}