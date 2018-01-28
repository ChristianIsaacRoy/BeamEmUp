using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : MonoBehaviour
{
    public GameData gameData;
    public ParticleSystem shootingParticleSystem;
    public GameObject gun;

    private GameObject zapTarget;
    private float elapsedZapTime = 0.0f;
    private bool playerIsShooting = false;
    private Vector3 shootingOrigin;

    /* Injected by PlayerController */
    [HideInInspector]
    public Player player;

    [HideInInspector]
    public int playerId;

    [HideInInspector]
    public Animator AnimController;

    [HideInInspector]
    public ShooterGameCamera shooterGameCamera;

    [HideInInspector]
    public GameManager gm;
    /* End Inject */

    private void Start()
    {
        shootingOrigin = gun.transform.position;   
    }

    private void Update()
    {
        CheckGunUse();

        if (zapTarget != null)
            CheckZappingProgress();
        else
            CheckNewShoot();

        // Increase or decrease elapsedZapTime based on zapTarget
        CheckZapTime();
    }

    private bool FireRay(float rayDistance, out RaycastHit raycastHit)
    {
        LayerMask ignoreMask = (LayerMask.NameToLayer("Player"));
        RaycastHit hit;
        Debug.DrawRay(gun.transform.position, shooterGameCamera.aimTarget.position - gun.transform.position, Color.green);

        if (Physics.SphereCast(gun.transform.position, 1.5f, 
            (shooterGameCamera.aimTarget.position - gun.transform.position).normalized, out hit, rayDistance, ignoreMask))
        {
            raycastHit = hit;
            return true;
        }

        raycastHit = new RaycastHit();
        return false;
    }

    private void CheckGunUse()
    {
        if (player.GetButtonDown("Shoot"))
        {
            playerIsShooting = true;
            shootingParticleSystem.Play();
            AnimController.SetBool("Shooting", true);
        }
        else if (player.GetButtonUp("Shoot"))
        {
            playerIsShooting = false;
            shootingParticleSystem.Stop();
            AnimController.SetBool("Shooting", false);
        }
    }

    private void CheckZapTime()
    {
        // Still zapping
        if (zapTarget != null)
        {
            elapsedZapTime += Time.deltaTime;
            if (elapsedZapTime >= gameData.timeToZap)
            {
                // Successful zap
                GameItem item = zapTarget.GetComponent<GameItem>();
                CancelShooting();
                elapsedZapTime = 0.0f;
                if (gm != null)
                {
                    gm.AddItemToPlayer(playerId, item.itemData);
                }
                item.ZapItem();
            }
        }
        // Stopped zapping, slowly decrease zap time
        else
        {
            elapsedZapTime -= Time.deltaTime;
            if (elapsedZapTime < 0)
            {
                elapsedZapTime = 0;
            }
        }
    }

    private void CheckZappingProgress()
    {
        // Make sure trigger is still held down
        if (!playerIsShooting)
        {
            CancelShooting();
            return;
        }

        // Make sure distance to zapTarget is still within zappingDistance
        float distanceToZapTarget = (zapTarget.transform.position - gun.transform.position).magnitude;
        if (distanceToZapTarget > gameData.distanceToZap)
        {
            CancelShooting();
            return;
        }

        // Make sure player is still looking at object and still within zapping distance
        RaycastHit hit;
        if(FireRay(distanceToZapTarget, out hit))
        {
            if (hit.transform.gameObject != zapTarget)
                CancelShooting();
        }
        else
        {
            CancelShooting();
        }
    }

    private void CancelShooting()
    {
        zapTarget.GetComponent<GameItem>().isBeingZapped = false;
        zapTarget = null;
    }

    private void CheckNewShoot()
    {
        float distance = gameData.distanceToZap;

        if (playerIsShooting)
        {
            RaycastHit hit;
            if (FireRay(distance, out hit))
            {
                GameItem item = hit.transform.gameObject.GetComponent<GameItem>();
                if (item != null)
                {
                    zapTarget = hit.transform.gameObject;
                    zapTarget.GetComponent<GameItem>().isBeingZapped = true;
                }
            }
        }
    }
}
