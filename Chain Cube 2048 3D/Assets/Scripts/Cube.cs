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

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private TextMeshPro[] posRanks;
    [SerializeField] private List<MatSettings> install = new List<MatSettings>();

    [SerializeField] private GameObject prefabCube;
    [SerializeField] private int rank;
    [SerializeField] private float forceValue;

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
        rb = GetComponent<Rigidbody>();
        cubeTail = GetComponent<TrailRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cubeTail != null)
        {
            if (cubeTail.enabled)
            {
                cubeTail.enabled = false;
            }
        }
        
        if (collision.gameObject.tag == "Cube")
        {
            col = collision.gameObject.GetComponent<Cube>();
            if (rank == col.rank)
            {
                Vector3 newCubeVector = (col.transform.position + transform.position) / 2f;
                Smash(col);
                transform.position = newCubeVector;
                GameObject closestCube = ClosestCube();
                if (closestCube != null)
                {
                    rb.velocity = closestCube.transform.position - transform.position;
                    rb.velocity = new Vector3(rb.velocity.x, forceValue/1.5f, rb.velocity.z);
                    if (rb.velocity.x > forceValue)
                    {
                        Debug.Log(11);
                        rb.velocity = new Vector3(forceValue, rb.velocity.y, rb.velocity.z);
                    }
                    if (rb.velocity.y > forceValue)
                    {
                        Debug.Log(12);
                        rb.velocity = new Vector3(rb.velocity.x, forceValue, rb.velocity.z);
                    }
                    if (rb.velocity.z > forceValue)
                    {
                        Debug.Log(13);
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

    private GameObject ClosestCube()
    {
        GameObject closest = null;
        allCubes = GameObject.FindGameObjectsWithTag("Cube");

        float dist = Mathf.Infinity;
        
        foreach (GameObject tmp in allCubes)
        {
            Cube tmpCube = tmp.gameObject.GetComponent<Cube>();
            if (tmpCube.rank == rank && tmp.transform.position != transform.position)
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
    }

    public void SetRank(int rank)
    {
        foreach (TextMeshPro text in posRanks)
        {
            text.text = rank.ToString();

            MatSettings settings = install.Find(tmp => tmp.rank == rank);

            if (settings == null)
                renderer.material = default;
            else
                renderer.material = settings.material;
        }
    }

    [System.Serializable]
    public class MatSettings
    {
        public int rank;
        public Material material;
    }
}