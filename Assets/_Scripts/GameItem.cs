using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public ItemData itemData;
    public float itemHoverHeight;
    public bool isBeingZapped;
    public float count;

    private MeshFilter itemFilter;

    public void Start()
    {
        itemFilter = GetComponent<MeshFilter>();
        itemFilter.mesh = itemData.itemMesh;
    }

    public void Update()
    {
        if (transform.tag == "Item")
        {
            count += 1;
            //item rotation
            transform.Rotate(0, 10 * Time.deltaTime, 0);
            //item float
            transform.Translate(0, Mathf.Sin(Time.fixedTime) / (100 / itemHoverHeight), 0);
        }

        if (!IsGrounded())
        {
            transform.position += Vector3.down * Time.deltaTime * 3;
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 3 + 0.1f);
    }

    public void ZapItem()
    {
        Destroy(this.gameObject);
    }

}
