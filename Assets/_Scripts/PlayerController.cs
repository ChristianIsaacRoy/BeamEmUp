using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float moveSpeed;

    public GameData gameData;
    private GameManager gameManager;

    public ParticleSystem particleSystem;

    private GameObject zapTarget;
    private float elapsedZapTime = 0.0f;

    [Range(1, 20)]
    public float jumpVelocity = 2f;

    public float jumpDecelerationSlow = 2f;
    public float jumpDecelertationFast = 2f;
    public float fallAcceleration = 2f;

    public float deadZone = 0.1f;

    public Camera myCamera;
    private ShooterGameCamera shooterGameCamera;

    private CharacterController cc;
    private Player player;
    private float distToGround;
    private Collider col;
    private bool canJump;

    public Vector3 MoveVector { get; set; }
    public Vector3 VertVector { get; set; }

    #region Monobehaviours
    public void Awake()
    {
        player = ReInput.players.GetPlayer(playerID);
        cc = GetComponent<CharacterController>();
        col = GetComponent<Collider>();
        distToGround = col.bounds.extents.y;
    }

    public void Start()
    {
        gameManager = GameManager.instance;

        if (myCamera == null)
            Debug.LogError("Player " + playerID + " is missing camera", this);
        else
            shooterGameCamera = myCamera.GetComponent<ShooterGameCamera>();
    }

    public void Update()
    {
        if (myCamera == null)
            return;

        CalculateVerticalMovement();
        GetLocomotionInput();
        SnapAlignCharacterWithCamera();
        ProcessMotion();

        if (zapTarget != null)
            CheckZappingProgress();
        else
            CheckShoot();

        // Increase or decrease elapsedZapTime based on zapTarget
        CheckZapTime();
    }
    #endregion

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
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
                ItemFunctionManager item = zapTarget.GetComponent<ItemFunctionManager>();
                CancelShooting();
                elapsedZapTime = 0.0f;
                if (gameManager != null)
                {
                    gameManager.AddItemToPlayer(playerID, item.itemData);
                }
                item.itemPickup();
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
        if (!player.GetButton("Shoot"))
        {
            CancelShooting();
            return;
        }

        // Make sure distance to zapTarget is still within zappingDistance
        Vector3 origin = myCamera.transform.position;
        origin.z = transform.position.z;
        float distanceToZapTarget = (zapTarget.transform.position - origin).magnitude;
        if (distanceToZapTarget > gameData.distanceToZap)
        {
            CancelShooting();
            return;
        }

        // Make sure player is still looking at object and still within zapping distance
        LayerMask ignoreMask = (LayerMask.NameToLayer("Player"));
        RaycastHit hit;
        Debug.DrawRay(origin, shooterGameCamera.aimTarget.position - origin, Color.green);

        // Change to SphereCast to allow for a margin of error from the player
        if (Physics.SphereCast(origin, 1.5f, (shooterGameCamera.aimTarget.position - origin).normalized, out hit, distanceToZapTarget, ignoreMask))
        { 
            if (hit.transform.gameObject != zapTarget)
            {
                CancelShooting();
                return;
            }
        }
        else
        {
            CancelShooting();
            return;
        }
    }

    private void CancelShooting()
    {
        particleSystem.Stop();
        zapTarget = null;
    }

    private void CheckShoot()
    {
        // Can't shoot yourself
        LayerMask ignoreMask = (LayerMask.NameToLayer("Player"));
        
        Vector3 origin = myCamera.transform.position;
        // Shift origin up to player position
        origin.z = transform.position.z;

        float distance = gameData.distanceToZap;

        if (player.GetButton("Shoot"))
        {
            RaycastHit hit;
            Debug.DrawRay(origin, shooterGameCamera.aimTarget.position - origin, Color.green);
            if (Physics.Raycast(origin, (shooterGameCamera.aimTarget.position - origin).normalized, out hit, distance, ignoreMask))
            {
                ItemFunctionManager item = hit.transform.gameObject.GetComponent<ItemFunctionManager>();
                if (item != null)
                {
                    zapTarget = hit.transform.gameObject;
                    particleSystem.Play();
                }
            }
        }
    }

    private void CalculateVerticalMovement()
    {
        if (IsGrounded())
        {
            VertVector = Vector3.zero;
            canJump = true;
        }

        if (cc.velocity.y < 0)
        {
            VertVector += Physics.gravity * fallAcceleration * Time.deltaTime;
        } else if (cc.velocity.y > 0 && player.GetButton("Jump") )
        {
            VertVector += Physics.gravity * jumpDecelerationSlow * Time.deltaTime;
        }
        else
        {
            VertVector += Physics.gravity * jumpDecelertationFast * Time.deltaTime;
        }
    }

    private void GetLocomotionInput()
    {
        MoveVector = Vector3.zero;

        if (player.GetAxis("MoveVertical") > deadZone || player.GetAxis("MoveVertical") < -deadZone)
        {
            MoveVector += new Vector3(0, 0, player.GetAxis("MoveVertical"));
        }

        if (player.GetAxis("MoveHorizontal") > deadZone || player.GetAxis("MoveHorizontal") < -deadZone)
        {
            MoveVector += new Vector3(player.GetAxis("MoveHorizontal"), 0, 0);
        }

        if (player.GetButtonDown("Jump") && canJump)
        {
            VertVector = Vector3.up * jumpVelocity;
            canJump = false;
        }
    }

    private void ProcessMotion()
    {
        MoveVector = transform.TransformDirection(MoveVector);

        if (MoveVector.magnitude > 1)
        {
            MoveVector = Vector3.Normalize(MoveVector);
        }

        MoveVector *= moveSpeed;
        MoveVector += VertVector;
        MoveVector *= Time.deltaTime;
        
        cc.Move(MoveVector);
    }

    private void SnapAlignCharacterWithCamera()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                              myCamera.transform.eulerAngles.y,
                                              transform.eulerAngles.z);
        //particleSystem.shape.rotation = transform.rotation;
    }

}
