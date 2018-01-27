using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public ItemData itemData;
    public int itemHoverHeight;
    public bool isBeingZapped;

    private MeshFilter itemFilter;

    public void Start()
    {
        itemFilter = GetComponent<MeshFilter>();
        itemFilter.mesh = itemData.itemMesh;
    }

    public void Update()
    {
        //item rotation
        transform.Rotate(0, 10 * Time.deltaTime, 0);
        //item float
        transform.Translate(0, Mathf.Sin(Time.fixedTime) / (100 / itemHoverHeight), 0);
    }

    public void ZapItem()
    {
        Destroy(this.gameObject);
    }

}
