using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public ItemData itemData;
    public float itemHoverHeight;
    public bool isBeingZapped;

    public void Start()
    {
        if (itemData != null)
        {
            Instantiate<GameObject>(itemData.newGameObject, transform);
        }
    }

    public void Update()
    {
        //item rotation
        transform.Rotate(0, 10 * Time.deltaTime, 0);
        //item float
        transform.Translate(0, Mathf.Sin(Time.fixedTime) / (100 / itemHoverHeight), 0);

        if (!IsGrounded())
        {
            transform.position += Vector3.down * Time.deltaTime * 3;
            if (transform.position.y < -20)
            {
                Destroy(gameObject);
            }
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
