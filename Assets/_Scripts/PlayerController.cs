using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float moveSpeed;

    
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
        CheckShoot();
    }
    #endregion

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    private void CheckShoot()
    {
        // Can't shoot yourself
        LayerMask ignoreMask = (LayerMask.NameToLayer("Player"));
        
        Vector3 origin = myCamera.transform.position;
        // Shift origin up to player position
        origin.z = transform.position.z;

        // TODO: Add editor-facing distance
        float distance = Mathf.Infinity;

        if (player.GetButton("Shoot"))
        {
            RaycastHit hit;
            Debug.DrawRay(origin, shooterGameCamera.aimTarget.position - origin, Color.green);
            if (Physics.Raycast(origin, shooterGameCamera.aimTarget.position - origin, out hit, distance, ignoreMask))
            {
                Debug.Log(hit.transform.gameObject.name);
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

    }

}
