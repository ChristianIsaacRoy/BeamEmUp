using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float moveSpeed;
    public float jumpHeight;
    public float jumpSpeed;

    public float gravity;

    public float deadZone = 0.1f;

    public Camera myCamera;

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
    }

    public void Update()
    {
        if (myCamera == null)
            return;

        CalculateVerticalMovement();
        GetLocomotionInput();
        SnapAlignCharacterWithCamera();
        ProcessMotion();
    }
    #endregion

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void CalculateVerticalMovement()
    {
        if (IsGrounded())
        {
            VertVector = Vector3.zero;
            canJump = true;
        }
        else
        {
            VertVector += Vector3.down * gravity;
            VertVector *= Time.deltaTime;
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


    }

    private void ProcessMotion()
    {
        MoveVector = transform.TransformDirection(MoveVector);

        if (MoveVector.magnitude > 1)
        {
            MoveVector = Vector3.Normalize(MoveVector);
        }

        MoveVector *= moveSpeed;
        MoveVector *= Time.deltaTime;

        MoveVector += VertVector;

        cc.Move(MoveVector);
    }

    private void SnapAlignCharacterWithCamera()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                                              myCamera.transform.eulerAngles.y,
                                              transform.eulerAngles.z);

    }

}
