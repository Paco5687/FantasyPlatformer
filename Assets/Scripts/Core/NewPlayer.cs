using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : MonoBehaviour // used to be PhysicsObject
{   
    [Header("Movement")]
    private CharacterController controller;
    private Vector2 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;

    [Header("Player Stats")]
    [SerializeField] public int playerArmor;
    [SerializeField] public float playerAttackDuration;                               // How long is the attack box active per mouse click
    [SerializeField] public int playerAttackSpeed;
    [SerializeField] public int playerAttackPower;
//    [SerializeField] public int playerBlock;
    [SerializeField] public int playerCritical;
//    [SerializeField] public int playerDefense;
//    [SerializeField] public int playerDodge;
//    [SerializeField] public int playerEnergy;
    [SerializeField] public int playerHealth = 100;
    [SerializeField] public int playerMaxHealth = 100;
    [SerializeField] public int playerMaxDamage;
    [SerializeField] public int playerMinDamage;
 //   private Inventory inven;

    /*[Header("Inventory")]
    public int ammo;
    public int coinsCollected;
    [SerializeField] public bool playerWeaponEquipped = false;
    [SerializeField] public string playerWeapon;
    */
    /*[Header("Movement")]
    [SerializeField] private bool crouch = false;                                 // Determines if the player is crouching
    [SerializeField] private float crouchSpeed = 2f;			  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private float jumpPower = 6;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] bool tryingToStand = false;
    [SerializeField] private GameObject crouchBox;
    public bool facingRight = true;
    [SerializeField]public float horizontalValue;
    [SerializeField] private float rayCastLength = 0.45f;
    private RaycastHit2D ceilingRaycastHit;
    [SerializeField] private LayerMask rayCastLayerMask; //Which layer do we want the raycast to interact with?
    */

    /*[Header("Shoot")]
    public Transform  gun;
    Vector2 direction;
    public GameObject laser;
    public float laserSpeed;
    public GameObject shootPoint;
    public float fireRate;
    public float readyForNextShot;
    */
    [Header("References")]
    public Animator animator;
    //[SerializeField] private GameObject attackBox;
    //[SerializeField] private GameObject gunHolder;
    //private Vector2 healthBarOrigSize;
    //public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); // Dictionary storing all inventory items, strings, values
    //public Dictionary<string, Sprite> weapon = new Dictionary<string, Sprite>();    // Dictionary storing all weapon items, strings, values
    //public Sprite inventoryItemBlank;                                               // Default inventory slot sprite
    //public Sprite keyGemSprite;                                                     // Key inventory item 
    //public Sprite keySprite;                                                        // Gem Key inventory item 
    //public Sprite weaponBlank;                                                      // Default weapon slot sprite

    /*Singleton instantiation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }*/

    // Awake is called before the Start function
    //private void Awake()
    //{
        //if (GameObject.Find("NewPlayer")) Destroy(gameObject);
        //inven = new Inventory();
    //}

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        //gameObject.name = "NewPlayer";

        //healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        //UpdateUI();

        //SetSpawnPosition();

        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //Crouch();
        PlayerMove();
        //Jump();
        //Flip();


        /* If we press Fire1 then set the attackBox to active. else set active to false
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }*/

        // Player dies
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    /* Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        if (playerWeaponEquipped == false)
        {
            attackBox.SetActive(true);
            yield return new WaitForSeconds(playerAttackDuration);
            attackBox.SetActive(false);
        }
    }*/

    /* Update UI elements
    public void UpdateUI()
    {
        // If healthBarOrigSize has not been set yet, match it with the healthBar rectTransform size
        if (healthBarOrigSize == Vector2.zero) healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        GameManager.Instance.coinsText.text = coinsCollected.ToString();
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)playerHealth / (float)playerMaxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }*/


    /* This is the old Character Controller with the Ridigbody2D
    // Player left and right movement
    void Move()
    {
        if (crouch == false)
        {
            horizontalValue = Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
            targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);
        } else
        {
            horizontalValue = Input.GetAxis("Horizontal") * crouchSpeed * Time.deltaTime;
            targetVelocity = new Vector2(Input.GetAxis("Horizontal") * crouchSpeed, 0);
        }
    }

    // If the player presses "Jump", set the velocity to a jump power value
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }
    }

    // Flip the players localScale.x if the move speed is > .01 or < -.01
    void Flip()
    {
        if ((horizontalValue < 0 && facingRight) || (horizontalValue > 0 && !facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    // Lets the player crouch, disabling the crouchCollider
    void Crouch()
    {
        ceilingRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.up, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.up * rayCastLength, Color.red);
        if (Input.GetButtonDown("Crouch") && grounded)
        {
            crouch = true;
            crouchBox.SetActive(false);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            if (ceilingRaycastHit.collider != null)
            {
                Debug.Log("I'm still crouching");
                tryingToStand = true;
            }
            else
            {
                crouchBox.SetActive(true);
                crouch = false;
            }
        }
        else if (ceilingRaycastHit.collider == null && tryingToStand)
        {
            crouchBox.SetActive(true);
            crouch = false;
            tryingToStand = false;
        }
    }
    */

    void PlayerMove()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector2.zero)
        {
            gameObject.transform.forward = move;
        }

        //Changes the height position of the player
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Debug.Log("Update is called.");
        Debug.Log("Move input: " + move);
    }


    // Player can die
    public void Die()
    {
        SceneManager.LoadScene("Level1");
    }

    /* Add Inventory Items
    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventoryImage.sprite = inventory[inventoryName];
    }*/

    /* Remove Inventory Items
    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        GameManager.Instance.inventoryImage.sprite = inventoryItemBlank;
    }*/

    /* Add Weapon
    public void AddWeapon(string weaponName, Sprite image = null)
    {
        weapon.Add(weaponName, image);
        GameManager.Instance.weaponImage.sprite = weapon[weaponName];
        //AttackBox.Instance.weaponRenderer.sprite = weapon[weaponName];
        playerWeaponEquipped = true;
        playerWeapon = Weapons.Instance.weaponStringName;
        gunHolder.SetActive(true);

    }*/

    // Remove Weapon
    /*public void RemoveWeapon(string weaponName)
    {
        weapon.Remove(weaponName);
        GameManager.Instance.weaponImage.sprite = weaponBlank;
        playerWeaponEquipped = false;
    }*/

    /* Spawn Position on Load Scene
    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }*/



}
    
