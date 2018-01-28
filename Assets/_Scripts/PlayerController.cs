using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID = 0;

    public float moveSpeed;

    public GameData gameData;
    private GameManager gm;

    private GameObject zapTarget;
    private float elapsedZapTime = 0.0f;
    private bool playerIsShooting = false;

    [Range(1, 20)]
    public float jumpVelocity = 2f;

    public float jumpDecelerationSlow = 2f;
    public float jumpDecelertationFast = 2f;
    public float fallAcceleration = 2f;

    public float deadZone = 0.1f;

    public Camera cam;
    private ShooterGameCamera shooterGameCamera;

    private CharacterController cc;
    private Player player;
    private float distToGround;
    private Collider col;
    private bool canJump;

    public Animator AnimController; 

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
        gm = GameManager.instance;

        if (gm == null)
        {
            if (cam != null)
                InstantiatePlayer(transform.position, cam);
            else
                Debug.LogError("Player " + (playerID + 1) + " is missing camera", this);
        }
    }

    public void Update()
    {
        if (cam == null)
            return;

        CalculateVerticalMovement();
        GetLocomotionInput();
        SnapAlignCharacterWithCamera();
        ProcessMotion();
    }
    #endregion

    public void InstantiatePlayer(Vector3 pos, Camera camera)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        cam = camera;
        shooterGameCamera = cam.GetComponent<ShooterGameCamera>();
    }

    public bool IsGrounded()
    {
 
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);

    }

    private void CalculateVerticalMovement()
    {
        if (IsGrounded())
        {
            AnimController.SetBool("isGrounded", true);
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

        AnimController.SetFloat("MoveSpeed", MoveVector.magnitude);


        if (player.GetButtonDown("Jump") && canJump)
        {
            VertVector = Vector3.up * jumpVelocity;
            canJump = false;
            AnimController.SetBool("isGrounded", false);
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
                                              cam.transform.eulerAngles.y,
                                              transform.eulerAngles.z);
    }

}
