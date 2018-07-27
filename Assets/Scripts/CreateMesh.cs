using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    private GameObject go;

    public List<GameObject> GOList;
    public Camera MainCamera;

    void Start()
    {
//        go = this.GetTriangle();
//        GetPentagon();
//
//        Gizmos.color = Color.white;
//        Gizmos.DrawWireSphere(transform.position, 5);



        CreateMeshTest();
    }
        
    // Update is called once per frame
    void Update()
    {
        return;
        Mesh mesh = go.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i] += normals[i] * Mathf.Sin(Time.time);
            i++;
        }
        mesh.vertices = vertices;
    }

    public GameObject GetTriangle()
    {
        GameObject go = new GameObject("Triangle");
        MeshFilter filter = go.AddComponent<MeshFilter>();
             
        // 构建三角形的三个顶点，并赋值给Mesh.vertices
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 1),
            new Vector3(0, 2, 0),
            new Vector3(2, 0, 5),
        };

        // 构建三角形的顶点顺序，因为这里只有一个三角形，
        // 所以只能是(0, 1, 2)这个顺序。
        mesh.triangles = new int[3] { 0, 1, 2 };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // 使用Shader构建一个材质，并设置材质的颜色。
        Material material = new Material(Shader.Find("Diffuse"));
        material.SetColor("_Color", Color.yellow);

        // 构建一个MeshRender并把上面创建的材质赋值给它，
        // 然后使其把上面构造的Mesh渲染到屏幕上。
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;

        return go;
    }

    public GameObject GetPentagon()
    {
        GameObject go = new GameObject("Pentagon");
        MeshFilter filter = go.AddComponent<MeshFilter>();
     
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
//        mesh.vertices = new Vector3[]
//        {
//            new Vector3(0, 0, 0),
//            new Vector3(0, 2, 0),
//            new Vector3(2, 0, 0),
//            new Vector3(2, -2, 0),
//            new Vector3(1, -2, 0),
//        };
//
//        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 4 };

//        mesh.vertices = new Vector3[]
//        {
//            new Vector3(2, 0, 0),
//            new Vector3(2, 2, 0),
//            new Vector3(0, 2, 0),
//            new Vector3(0, 0, 0),
//
//        };
//        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        int len = 5;
        int gap = 2;
        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, len, 0),
            new Vector3(len, len, 0),
            new Vector3(len, 0, 0),

            new Vector3(0, 0, 0),
            new Vector3(0, gap, 0),
            new Vector3(gap, gap, 0),
            new Vector3(gap, 0, 0),
        };
        mesh.triangles = new int[]
        {
            6, 3, 7, 
            2, 3, 6,
            1, 2, 6,
//            5, 1, 6
            1, 6, 5 
        };


//        mesh.triangles = new int[]{ 5, 1, 6 };
        //        mesh.triangles = new int[]{ 1, 6, 5  };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        Material material = new Material(Shader.Find("Diffuse"));
        material.SetColor("_Color", Color.red);

        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;

        return go;
    }


    private  const int SquareLen = 5;
    public List<Vector3> SquarePosList = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(0, SquareLen, 0),
        new Vector3(SquareLen, SquareLen, 0),
        new Vector3(SquareLen, 0, 0),
    };

    //    private List<Vector3> InnerPosList = new List<Vector3>
    //    {
    //        new Vector3(1, 1, 0),
    //        new Vector3(0.5f, 1.5f, 0),
    ////        new Vector3(0.3f, 1.8f, 0),
    //        new Vector3(1, 2, 0),
    //        new Vector3(2, 2, 0),
    //        new Vector3(2.5f, 1.5f, 0),  
    //        new Vector3(2, 1, 0),
    //    };

    private List<Vector3> InnerPosList = new List<Vector3>
    {
        new Vector3(1, 1, 0),
        new Vector3(1.6f, 1.6f, 0),
        new Vector3(1, 2, 0),
        new Vector3(2, 2, 0),
        new Vector3(1.7f, 1.5f, 0),  
        new Vector3(2, 1, 0),
    };

    //    private List<Vector3> InnerPosList = new List<Vector3>
    //    {
    //        new Vector3(1, 1, 0),
    //        new Vector3(1, 2, 0),
    //        new Vector3(1.2f, 2, 0),
    //        new Vector3(1.2f, 1.5f, 0),
    //        new Vector3(1.8f, 1.5f, 0),
    //        new Vector3(1.8f, 2, 0),
    //        new Vector3(2, 2, 0),
    //        new Vector3(2, 1, 0),
    //    };

    public GameObject CreateMeshTest()
    {
        GameObject go = new GameObject("Mine");
        MeshFilter filter = go.AddComponent<MeshFilter>();
     
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
        var vers = new List<Vector3>();
        vers.AddRange(SquarePosList);
        vers.AddRange(InnerPosList);
        mesh.vertices = vers.ToArray();

//        int i = 0;
//        foreach (var item in vers)
//        {
//            Debug.Log("idx:" + i + ";pos:" + item.ToString());
//            i++;
//        }
        var trains = Triangulation.GetHoleTriangleIdx(SquarePosList, InnerPosList);

//        trains.Add(6);
//        trains.Add(5);
//        trains.Add(0);

        mesh.triangles = trains.ToArray();


        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        Material material = new Material(Shader.Find("Diffuse"));
        material.SetColor("_Color", Color.red);

        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;
        return go;
    }

    void OnEnable()
    {
//        foreach (var item in GOList)
//        {
//            var pos = item.gameObject.transform.position;
//            var posLocal = item.gameObject.transform.localPosition;
//            var posScreen = MainCamera.WorldToScreenPoint(pos);
//            Debug.Log("pos:" + pos + ";posLocal:" + posLocal + ";posScreen:" + posScreen);
//        }
    }
}