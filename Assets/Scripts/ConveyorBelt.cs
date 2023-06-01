using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed, conveyorSpeed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private List<GameObject> onBelt;

    private Material material;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}