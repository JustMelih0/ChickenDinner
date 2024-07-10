using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    #region Movement

        public float moveSpeed;
        [SerializeField]private bool facingRight;

    #endregion

    #region Jump

        [SerializeField]private float footRadius;
        [SerializeField]private float jumpForce;
        [SerializeField]private float fallGravity;
        private float startGravity;
        [SerializeField]private LayerMask groundLayer;
        [SerializeField]private Transform footPoint;

    #endregion

    #region Inputs

        private float inputX;
        private InputAction moveAction;
        private InputAction jumpAction;

    #endregion

    #region Components

        private Rigidbody2D rigidBody2D;
        private PlayerController playerController;
        private Animator anim;

    #endregion



    private void Awake()
    {
        var inputActionAsset=GetComponent<PlayerInput>().actions;

        moveAction=inputActionAsset.FindAction("HorizontalMove");
        jumpAction=inputActionAsset.FindAction("Jump");
    }
    private void OnEnable() 
    {
        moveAction.Enable();
        jumpAction.Enable();
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveCanceled;
        jumpAction.performed += OnJumpInput;
    }
    private void OnDisable() 
    {
        moveAction.Disable();
        jumpAction.Disable();
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveCanceled;
        jumpAction.performed -= OnJumpInput;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        startGravity = rigidBody2D.gravityScale;
    }


    void FixedUpdate()
    {
        Move();
    }
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (CheckGround())
        {
            Jump();
        }
    }
    private void Update() 
    {
        Anime();
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        float inputAxis = context.ReadValue<float>();
        inputX=inputAxis;
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
       inputX=0;
    }
    private void Anime()
    {
        anim.SetFloat("speed", Mathf.Abs(rigidBody2D.velocity.x));
        float clampedAirSpeed = Mathf.Clamp(rigidBody2D.velocity.y, -1f, 1f);
        anim.SetFloat("airSpeed", clampedAirSpeed);
        anim.SetBool("isGrounded", CheckGround());
    }
    void Move()
    {
        if(playerController.canMove==false)
        return;
        
        rigidBody2D.velocity=new Vector2(inputX*moveSpeed,rigidBody2D.velocity.y);

        if(rigidBody2D.velocity.y<0)
        {
            rigidBody2D.gravityScale=fallGravity;
        }
        else
        {
            rigidBody2D.gravityScale=startGravity;
        }

        CharacterFlip();
    }
    void CharacterFlip()
    {   
        if (facingRight&&rigidBody2D.velocity.x<0||facingRight==false&&rigidBody2D.velocity.x>0)
        {
            facingRight=!facingRight;
            transform.localRotation = Quaternion.Euler(0, facingRight == false ? 0 : 180, 0);
        }

    }
    void Jump()
    {
       if(playerController.canMove==false)
       return;

       rigidBody2D.velocity=new Vector2(rigidBody2D.velocity.x,jumpForce);
    }
    
    bool checkGround=false;
    bool CheckGround()
    {
        checkGround=Physics2D.OverlapCircle(footPoint.position,footRadius,groundLayer);

        return checkGround;
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(footPoint.position,footRadius);
    }
}
