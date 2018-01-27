using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunctionManager : MonoBehaviour {

    public int pointValue;
    public ItemData itemData;
    private MeshFilter itemFilter;
	// Use this for initialization
	void Start () {
        itemFilter = GetComponent<MeshFilter>();
        itemFilter.mesh = itemData.itemMesh;
        pointValue = itemData.pointValue;
	}

    public void itemPickup()
    {
        //makes item dissapear (get zapped and pulled in)

        Destroy(this.gameObject);
    }
 
	
	// Update is called once per frame
	void Update () {
        //item rotation
        transform.Rotate(0, 10 * Time.deltaTime, 0);
        //item float
        transform.Translate(0, Mathf.Sin(Time.fixedTime)/60, 0);
	}
}
