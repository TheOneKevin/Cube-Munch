using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateMesh : MonoBehaviour {

    #region Variables

    public GameObject a, b, c, d, e; //Prefabs 1, 2, 3, 4 and 5
    public GameObject startingObject; //Hard-coded first object on the scene
    public Vector3[] verticies;
    public Vector3 offset;
    Dictionary<int, Vector3> dMesh = new Dictionary<int, Vector3>();
    Dictionary<int, Vector3[]> excl = new Dictionary<int, Vector3[]>();
    Dictionary<int, Vector3[]> zeroTolerance = new Dictionary<int, Vector3[]>();
    private int lastShape;
    private Vector3 posLastShape;

    #endregion

    #region Unity Stuff

    // Use this for initialization
    void Start ()
    {
        setDictionaryValues();
        verticies = GetVerticesInChildren(startingObject);
        offset = startingObject.GetComponent<MeshRenderer>().bounds.center;
        lastShape = 1;
        //addShape();
        System.Random r = new System.Random();
        int recursions = r.Next(2, 10);
        while(recursions >= 0)
        {
            addShape();
            recursions--;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    #endregion

    #region Trivial Functions

    Vector3[] GetVerticesInChildren(GameObject go)
    {
        MeshFilter[] mfs = go.GetComponentsInChildren<MeshFilter>();
        List<Vector3> vList = new List<Vector3>();
        foreach (MeshFilter mf in mfs)
        {
            vList.AddRange(mf.mesh.vertices); //Get all the points
        }
        int p = 0;
        Vector3[] temp = vList.ToArray();
        while (p < temp.Length)
        {
            temp[p] = new Vector3(temp[p].x * go.transform.localScale.x, temp[p].y * go.transform.localScale.y, temp[p].z * go.transform.localScale.z); //Convert to world space
            p++;
        }
        temp = Array.FindAll(temp, x => x.y == 1);
        return temp.Distinct().ToArray(); ;
    }

    private void setDictionaryValues()
    {
        dMesh.Add(1, new Vector3(-5, 0, 5)); dMesh.Add(6, new Vector3(5, 0, 3)); //"U" Shape
        dMesh.Add(2, new Vector3(-3, 0, 3)); dMesh.Add(7, new Vector3(3, 0, -3)); //Cube Shape
        dMesh.Add(3, new Vector3(-5, 0, 5)); dMesh.Add(8, new Vector3(3, 0, -3)); //"L" Shape
        dMesh.Add(4, new Vector3(-5, 0, 5)); dMesh.Add(9, new Vector3(5, 0, -3)); //"S" Shape
        dMesh.Add(5, new Vector3(-5, 0, 5)); dMesh.Add(10, new Vector3(3, 0, -5)); //"-|" shape
        
        excl.Add(1, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        excl.Add(2, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        excl.Add(3, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        excl.Add(4, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        excl.Add(5, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        
        zeroTolerance.Add(1, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        zeroTolerance.Add(2, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        zeroTolerance.Add(3, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
        zeroTolerance.Add(4, new Vector3[] { new Vector3(), new Vector3(), new Vector3(), new Vector3() });
    }

    private Vector3 rotateXZ(Vector3 rotPoint, int deg)
    {
        switch (deg)
        {
            case 1:
                return new Vector3(rotPoint.z, 0, 0 - rotPoint.x);
            case 2:
                return new Vector3(0 - rotPoint.x, 0, 0 - rotPoint.y);
            case 3:
                return new Vector3(0 - rotPoint.z, 0, rotPoint.x);
            case 4:
                return rotPoint;
            default:
                return rotPoint;
        }
    }

    #endregion

    #region Mesh Generation Function

    private void addShape()
    {
        System.Random r = new System.Random();
        int key = r.Next(1, 10) - 5; //Pick random shape minus 5
        GameObject[] shapes = new GameObject[]{a, b, c, d, e};
        //Get random value from hashmap.
        key = (key < 0) ? key + 5 : key; //See if key is negative. If so, add 5 to the value
        Vector3 posCurrentShape = new Vector3(shapes[lastShape].GetComponent<MeshRenderer>().bounds.size.x,
            1, shapes[key].GetComponent<MeshRenderer>().bounds.size.z);
        offset = shapes[key].GetComponent<MeshRenderer>().bounds.center;
        //posCurrentShape = new Vector3(UnityEngine.Random.Range(posCurrentShape.x, posCurrentShape.x), 0, UnityEngine.Random.Range(posCurrentShape.z, posCurrentShape.z));
        GameObject.Instantiate(shapes[key], posCurrentShape, Quaternion.Euler(270,0,0));
        lastShape = key;
    }

    #endregion

}
