using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Test : MonoBehaviour
{
	 [Header("Input")]

    // horizontal movement speed and direction
    public Vector2 playerInput;
    // boolean to detect / test jump input
    [SerializeField] bool jumpPress = false;
    [SerializeField] bool jumpHold = false;


    [Header("Parameters")]

    [SerializeField] private LayerMask groundLayer; // A mask determining what is ground to the character
    [SerializeField] private Transform ceilingCheck; // A position marking where to check for ceilings
    [SerializeField] private Transform groundCheck; // A position marking where to check if the player is isGrounded.

    private Vector3 velocity = Vector3.zero;
    public float runSpeed = 30f;
    [SerializeField] private float jumpForce = 6.2f; // Force added when the player jumps
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 3f;

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
    [SerializeField] private bool airControl = true; // Whether or not a player can steer while jumping

    const float isGroundedRadius = .2f; // Radius of the overlap circle to determine if isGrounded

    public float rememberGroundedFor = 0.1f;
    float lastTimeGrounded;

    private Rigidbody2D rb2d;


    [Header("Display")]

    public Animator animator;
    [SerializeField] bool facingRight = true;  // For determining which way the player is currently facing.
    public bool isGrounded; // is the player on the ground?


    private void Awake()
    {
        // get components, stop game if not found
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (!rb2d || !animator)
        {
            Debug.LogError("Rigidbody2D and Animator required");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        // get child objects, stop game if not found
        groundCheck = transform.Find("GroundCheck").gameObject.transform;
        ceilingCheck = transform.Find("CeilingCheck").gameObject.transform;
        if (!groundCheck || !ceilingCheck)
        {
            Debug.LogError("GroundCheck and CeilingCheck required");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private void Update()
    {
        // get horizontal input (between -1 and 1) from player
        playerInput.x = Input.GetAxisRaw("Horizontal");
        Move(playerInput.x * runSpeed * Time.fixedDeltaTime);

        // check if jump keys / buttons are pressed on this loop
        jumpPress = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow);
        Jump();
        // check if jump keys / buttons are held down
        jumpHold = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
        BetterJump();

        // whether currently on ground (collision detection)
        CheckIfGrounded();
    }

    public void Move(float move)
    {
        // set the speed variable
        animator.SetFloat("Speed", Mathf.Abs(move));

        //only control the player if isGrounded or airControl is turned on
        if (isGrounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rb2d.velocity.y);
            // And then smoothing it out and applying it to the character
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if ((move > 0 && !facingRight) || (move < 0 && facingRight))
            {
                Flip();
            }
        }
    }

    public void Jump()
    {
        // if jump button pressed + either grounded or late jump off edge 
        if (jumpPress && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            // Add a vertical force to the player 
            // => Brackeys version: e.g. jumpForce = 550, rb2d.gravityScale = 3
            //rb2d.AddForce(new Vector2(0f, jumpForce));

            // Add a vertical force to the player 
            // => craftgames version: e.g. jumpForce = 6, rb2d.gravityScale = 1
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

            // reset for late jump
            lastTimeGrounded = Time.time;
            animator.SetBool("IsJumping", true);
        }
    }

    // adjust falling and jumping speed based on whether player still has jump pressed
    // for more see: https://www.youtube.com/watch?v=7KiK0Aqtmzc
    void BetterJump()
    {
        // slow down velocity for all
        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        // slow up velocity if they release early
        else if (rb2d.velocity.y > 0 && !jumpHold)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckIfGrounded()
    {
        // is currently grounded?
        bool wasGrounded = isGrounded;
        isGrounded = false;

        // Check if a circle cast to the groundcheck position hits anything designated as Ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, isGroundedRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                // if not parent collider then set true 
                isGrounded = true;
                // if wasn't already on ground then it just landed
                if (!wasGrounded)
                {
                    //Debug.Log("collided with: " + colliders[i].gameObject.name);
                    OnLanding();
                }
            }
        }
    }

    void OnLanding()
    {
        //Debug.Log("OnLanding");
        animator.SetBool("IsJumping", false);
    }
/*
    [SerializeField] private float m_JumpForce = 850f;							// Amount of force added when the player jumps.
	//[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	//[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	//[Header("Events")]
	//[Space]

	//public UnityEvent OnLandEvent;

	//[System.Serializable]
	//public class BoolEvent : UnityEvent<bool> { }

	//public BoolEvent OnCrouchEvent;
	//private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	//public void Move(float move, bool crouch, bool jump)
	public void Move(float move, bool jump)
	{
/*
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}
*/
/*
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} 

			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}
        }
		
    }
*/






/*



    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Animator animator;
    string animationState = "AnimationState";
    Rigidbody2D rb2D;
    enum CharStates{dinoIdle = 1, dinoRun = 2, dinoJump = 3}
    
    private void Start(){
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update(){
        UpdateState();
    }
    void FixedUpdate(){
        MoveCharacter();
    }
    private void MoveCharacter(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb2D.velocity = movement*movementSpeed;
    }

    private void UpdateState(){
        if(movement.x > 0){
            animator.SetInteger(animationState, (int)CharStates.dinoRun);
        }
        else if(movement.x < 0){
            transform.Rotate(0f, 180f, 0f);
            animator.SetInteger(animationState, (int)CharStates.dinoRun);
        }
        else if(movement.y != 0){
            animator.SetInteger(animationState, (int)CharStates.dinoJump);
        }
        else{
            animator.SetInteger(animationState, (int)CharStates.dinoIdle);
        }
        
    }
*/
    
}
