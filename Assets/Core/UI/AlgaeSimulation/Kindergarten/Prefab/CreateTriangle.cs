using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class CreateTriangle : MonoBehaviour
{
    void Start()
    {
        GenerateMesh();
    }

    void OnValidate()
    {
        GenerateMesh();
    }

    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[3]
        {
            new Vector3(-0.5f, 0, 0),
            new Vector3(0.5f, 0, 0),
            new Vector3(0, 1, 0.5f)
        };

        int[] triangles = new int[3]
        {
            0, 1, 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
