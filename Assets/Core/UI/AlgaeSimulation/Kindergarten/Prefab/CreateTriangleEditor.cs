using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject))]
public class CreateTriangleEditor : Editor
{
    [MenuItem("GameObject/Create Triangle Face", false, 10)]
    static void CreateTriangleFace()
    {
        GameObject triangleFace = new GameObject("TriangleFace");
        triangleFace.AddComponent<MeshFilter>();
        triangleFace.AddComponent<MeshRenderer>();
        CreateTriangle createTriangle = triangleFace.AddComponent<CreateTriangle>();
        createTriangle.GenerateMesh();
    }
}
