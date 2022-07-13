using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;
    private TrailRenderer cubeTail;
    private Cube col;

    [SerializeField] private GameObject prefabCube;
    [SerializeField] private int rank;

    private void Start()
    {
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
                Debug.Log(1);
                //Vector3 newCubeVector = (col.transform.position + transform.position) / 2f;
                Destroy(col.gameObject);
                //transform.position = newCubeVector;
                rb.AddForce(Vector3.up * 20f, ForceMode.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
         
    }
}