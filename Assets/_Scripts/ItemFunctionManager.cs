using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFunctionManager : MonoBehaviour {

    public int pointValue;
    public ItemData itemData;
    private MeshFilter itemFilter;
    public int itemHoverHeight;
    private MeshCollider itemCollider;
    public bool gotZapped;

	// Use this for initialization
	void Start () {
        itemFilter = GetComponent<MeshFilter>();
        itemFilter.mesh = itemData.itemMesh;
        itemCollider = GetComponent<MeshCollider>();
        itemCollider.sharedMesh = itemData.itemMesh;
        pointValue = itemData.pointValue;
	}

    public void itemPickup()
    {
        gotZapped = true;
        //makes item dissapear (get zapped and pulled in)
        
        Destroy(this.gameObject);
    }
 
	
	// Update is called once per frame
	void Update () {
        //item rotation
        transform.Rotate(0, 10 * Time.deltaTime, 0);
        //item float
        transform.Translate(0, Mathf.Sin(Time.fixedTime) / (100 / itemHoverHeight), 0);
	}
}
