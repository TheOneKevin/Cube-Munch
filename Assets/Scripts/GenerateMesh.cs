using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateMesh : MonoBehaviour {

    //public Transform a, b, c, d, e; //Prefabs 1, 2, 3, 4 and 5
    public GameObject startingObject; //Hard-coded first object on the scene
    public Vector3[] verticies;
    Dictionary<int, Vector3> dictionary = new Dictionary<int, Vector3>();
    Dictionary<int, Vector3> excl = new Dictionary<int, Vector3>();

    // Use this for initialization
    void Start ()
    {
        verticies = GetVerticesInChildren(startingObject);
        /*int repeats = UnityEngine.Random.Range(8, 15); //Set a random # of repeats
        var list = new List<Vector3>(); //Set a null list
        verticies = startingObject.GetComponent<MeshFilter>().mesh.vertices; //Set variable
        Transform instantiatedObject = startingObject; //Set the first object added as the first object on the scene
        while (repeats >= 0)
        {
            //Combine the two arrays together
            list.AddRange(verticies);
            list.AddRange(instantiatedObject.GetComponent<MeshFilter>().mesh.vertices);
            verticies = list.ToArray(); //Convert it to an array
            repeats--; //Decrease counter and repeat process
        }*/
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

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
        dictionary.Add(1, new Vector3(-5, 0, 5)); dictionary.Add(1, new Vector3(5, 0, 3)); //"U" Shape
        dictionary.Add(2, new Vector3(-3, 0, 3)); dictionary.Add(2, new Vector3(3, 0, -3)); //Cube Shape
        dictionary.Add(3, new Vector3(-5, 0, 5)); dictionary.Add(3, new Vector3(3, 0, -3)); //"L" Shape
        dictionary.Add(4, new Vector3(-5, 0, 5)); dictionary.Add(4, new Vector3(5, 0, -3)); //"S" Shape
        dictionary.Add(5, new Vector3(-5, 0, 5)); dictionary.Add(5, new Vector3(3, 0, -5)); //"-|" shape

        excl.Add(1, new Vector3());
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
}
