using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public ItemData itemData;
    public float itemHoverHeight;
    public bool isBeingZapped;

    private Vector3 originalPosition;

    [HideInInspector]
    public GameObject playerZapping;

    public void Start()
    {
        if (itemData != null)
        {
            Instantiate<GameObject>(itemData.newGameObject, transform);
        }
        originalPosition = itemData.newGameObject.transform.position;
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

        if (isBeingZapped)
        {
            if (playerZapping != null)
            {
                // Check distance
                Vector3 gunPos = playerZapping.GetComponent<Zapper>().gun.transform.position;
                float distanceToGun = (transform.position - gunPos).magnitude;
                // Don't move if too close
                if (distanceToGun < 4.0f)
                {
                    return;
                }

                transform.position = Vector3.Slerp(transform.position, gunPos, Time.time * 0.001f);
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
