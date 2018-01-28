using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.MultiAudioListener;

public class GameItem : MonoBehaviour
{
    public MultiAudioSource audioSource;
    public ItemData itemData;
    public float itemHoverHeight;
    public bool isBeingZapped;

    public Material material;

    private Vector3 originalPosition;
    private bool hasBeenZapped = false;

    public bool isBoss;
    private int bossHits = 7;

    [HideInInspector]
    public GameObject playerZapping;

    public void Start()
    {
        if (this.CompareTag("Item"))
        {
            if (itemData != null)
            {
                Instantiate<GameObject>(itemData.newGameObject, transform);
            }
            originalPosition = transform.position;
        }
    }

    public void Update()
    {
        if (this.CompareTag("Item"))
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
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 3 + 0.1f);
    }

    public void ZapItem()
    {
        if (isBoss)
        {
            bossHits--;
            if (bossHits <= 0)
                StartCoroutine(DestroyItem());
        }
        else
        {
            StartCoroutine(DestroyItem());
        }
    }
    public IEnumerator DestroyItem()
    {
        player.StopVibration();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.AudioClip.length / 2);
        Destroy(this.gameObject);
    }

}
