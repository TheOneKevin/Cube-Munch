using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class GenerateMesh : MonoBehaviour
{

    #region Variables

    public GameObject a, b, c1, c2, c3, d1, d2, d3, e;
    public GameObject startingObj; //Must always be object [a]!!
    public string[] keys; public string shape; public int direction; public Vector3 lastPos; //Public for debug purposes

    //private Dictionary<string, Dictionary<string, int[]>> hashMap = new Dictionary<string, Dictionary<string, int[]>>();
    private Dictionary<string, int[]> shapeDirection = new Dictionary<string, int[]>();

    #endregion

    #region Unity Defaults

    // Use this for initialization
    void Start ()
    {
        initDictionary();
        addShape("a");
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    #endregion

    #region Init Methods

    /* (1, 2, 3, 4): up, right, down, left.
       shape: matching-shape -> {directions-ppossible}, matching-shape -> {dir}, etc.
        a: a->{1234}, b->{1234}, c1->{234}, c2->{4}, c3->{4}, d1->{123}, d2,3->{1} e->{14}
        b: a->{1234}, c1->{4}, c2->{2}, c3->{4}, d1->{13}, d2->{13}, e->{123}
        c1: a->{234}, b->{4}, c1->{24}, d1->{2}, d3->{4}, e->{3}
        c2: a->{4}, b->{2}, c2->{24}, d1->{2}, d3->{4}, e->{24}
        c3: a->{4}, b->{4}, c3->{24}, d1->{2}, d3->{4}, e->{1}
        d1: a->{123}, b->{13}, c1,2,3->{2}, d1->{13}, e->{2}
        d2: a->{1}, b->{13}, d2->{13}, e->{13}
        d3: a->{1}, c1,2,3->{4}, e->{4}
        e: a->{14}, b->{123}, c1->{3}, c2->{24}, c3->{1}, d1->{2}, d2->{13}, d3->{4}, e->{1234}
    */

    private void initDictionary()
    {
        //shape a and its corresponding matching shapes
        shapeDirection.Add("a-a", new int[] { 1, 2, 3, 4 }); shapeDirection.Add("a-b", new int[] { 1, 2, 3, 4 }); shapeDirection.Add("a-c1", new int[] { 2, 3, 4 });
        shapeDirection.Add("a-c2", new int[] { 4 }); shapeDirection.Add("a-c3", new int[] { 4 }); shapeDirection.Add("a-d1", new int[] { 1, 2, 3 });
        shapeDirection.Add("a-d2", new int[] { 1 }); shapeDirection.Add("a-d3", new int[] { 1 }); shapeDirection.Add("a-e", new int[] { 1, 4 });
        //shape b and its corresponding matching shapes
        shapeDirection.Add("b-a", new int[] { 1, 2, 3, 4 }); shapeDirection.Add("b-c1", new int[] { 4 }); shapeDirection.Add("b-c2", new int[] { 2 });
        shapeDirection.Add("b-c3", new int[] { 4 }); shapeDirection.Add("b-d1", new int[] { 1, 3 }); shapeDirection.Add("b-d2", new int[] { 1, 3 });
        shapeDirection.Add("b-e", new int[] { 1, 2, 3 });
        //shape c1 and its corresponding matching shapes
        shapeDirection.Add("c1-a", new int[] { 2, 3, 4 }); shapeDirection.Add("c1-b", new int[] { 4 }); shapeDirection.Add("c1-c1", new int[] { 4 });
        shapeDirection.Add("c1-d1", new int[] { 2 }); shapeDirection.Add("c1-d3", new int[] { 4 }); shapeDirection.Add("c1-e", new int[] { 3 });
        //shape c2 and its corresponding matching shapes
        shapeDirection.Add("c2-a", new int[] { 4 }); shapeDirection.Add("c2-b", new int[] { 2 }); shapeDirection.Add("c2-c2", new int[] { 2, 4 });
        shapeDirection.Add("c2-d1", new int[] { 2 }); shapeDirection.Add("c2-d3", new int[] { 4 }); shapeDirection.Add("c2-e", new int[] { 2, 4 });
        //shape c3 and its corresponding matching shapes
        shapeDirection.Add("c3-a", new int[] { 4 }); shapeDirection.Add("c3-b", new int[] { 4 }); shapeDirection.Add("c3-c3", new int[] { 2, 4 });
        shapeDirection.Add("c3-d1", new int[] { 2 }); shapeDirection.Add("c3-d3", new int[] { 4 }); shapeDirection.Add("c3-e", new int[] { 1 });
        //shape d1 and its corresponding matching shapes
        shapeDirection.Add("d1-a", new int[] { 1, 2, 3 }); shapeDirection.Add("d1-b", new int[] { 1, 3 }); shapeDirection.Add("d1-c1", new int[] { 2 });
        shapeDirection.Add("d1-c2", new int[] { 2 }); shapeDirection.Add("d1-c3", new int[] { 2 }); shapeDirection.Add("d1-d1", new int[] { 1, 3 });
        shapeDirection.Add("d1-e", new int[] { 2 });
        //shape d2 and its corresponding matching shapes
        shapeDirection.Add("d2-a", new int[] { 1 }); shapeDirection.Add("d2-b", new int[] { 1, 3 }); shapeDirection.Add("d2-d2", new int[] { 1, 3 });
        shapeDirection.Add("d2-e", new int[] { 1, 3 });
        //shape d3 and its corresponding matching shapes
        shapeDirection.Add("d3-a", new int[] { 1 }); shapeDirection.Add("d3-c1", new int[] { 4 }); shapeDirection.Add("d3-c2", new int[] { 4 });
        shapeDirection.Add("d3-c3", new int[] { 4 }); shapeDirection.Add("d3-e", new int[] { 4 });
        //shape e and its corresponding matching shapes
        shapeDirection.Add("e-a", new int[] { 1, 4 }); shapeDirection.Add("e-b", new int[] { 1, 2, 3 }); shapeDirection.Add("e-c1", new int[] { 3 });
        shapeDirection.Add("e-c2", new int[] { 2, 4 }); shapeDirection.Add("e-c3", new int[] { 1 }); shapeDirection.Add("e-d1", new int[] { 2 });
        shapeDirection.Add("e-d2", new int[] { 1, 3 }); shapeDirection.Add("e-d3", new int[] { 4 }); shapeDirection.Add("e-e", new int[] { 1, 2, 3, 4 });
    }

    #endregion

    #region Functions

    private void addShape(string shapeBefore)
    {
        System.Random r = new System.Random();
        keys = shapeDirection.Keys.ToArray(); //Get list of all shape-combos
        //Split the shape-combo and prune the array of any shapes that is not shapeBefore
        keys = keys.Where(key => Regex.Split(key, "(-)")[0] == shapeBefore).ToArray();
        string _key = keys[r.Next(0, keys.Length)]; //get a random number
        int[] _temp;
        shape = Regex.Split(_key, "(-)")[2]; //get the matching shape from the combo
        shapeDirection.TryGetValue(_key, out _temp); //get the corresponding directionS
        direction = _temp[r.Next(0, _temp.Length)]; //get a random direction
        //So now we have the direction and a shape we can spawn

    }

    #endregion

}
