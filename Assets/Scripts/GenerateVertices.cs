using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateVertices : MonoBehaviour
{
    public Mesh mesh;
    public Vector3[] vertices;
    public Vector3[] normals;
    public int CenterPoint;
    public int verticesCount;

    public List<GameObject> points;
    public GameObject toBeInstantiated;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
        verticesCount = vertices.Length;

        for (int i = 0; i < vertices.Length; i++)
        {
            {
                GameObject childObject = Instantiate(toBeInstantiated, gameObject.transform.position + vertices[i], Quaternion.LookRotation(normals[i])) as GameObject;
                childObject.transform.parent = gameObject.transform;
                points.Add(childObject);
            }
        }

        for (int i = 0; i < points.Count; i++)
        {
            {
                if (i == points.Count - 1)
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[0].GetComponent<Rigidbody2D>();
                }
                else
                {
                    points[i].GetComponent<HingeJoint2D>().connectedBody = points[i + 1].GetComponent<Rigidbody2D>();
                }
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = points[i].transform.localPosition;
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
    }
}
