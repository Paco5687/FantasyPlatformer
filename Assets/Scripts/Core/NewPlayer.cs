using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] private float attackDuration; //How long is the attack box active per mouse click
    public int attackPower = 50;
    [SerializeField] private float jumpPower = 6;
    public int health = 100;
    public int playerMaxHealth = 100;
    [SerializeField] float maxSpeed = 1;

    [Header("Inventory")]
    public int ammo;
    public int coinsCollected;

    [Header("References")]
    [SerializeField] private GameObject attackBox;
    private Vector2 healthBarOrigSize;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); // Dictionary storing all inventory items, strings, values
    public Sprite inventoryItemBlank; // Default inventory slot sprite
    public Sprite keyGemSprite; // Key inventory item 
    public Sprite keySprite; // Gem Key inventory item 

    //Singleton instantiation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    // Awake is called before the Start function
    private void Awake()
    {
        if (GameObject.Find("NewPlayer")) Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);
        gameObject.name = "NewPlayer";


        //healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();

        SetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);
        //If the player presses "Jump", set the velocity to a jump power value
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        //Flip the players localScale.x if the move speed is > .01 or < -.01
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        //If we press Fire1 then set the attackBox to active. else set active to false
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }

        //Player dies
        if (health <= 0)
        {
            Die();
        }

    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    //Update UI elements
    public void UpdateUI()
    {
        // If healthBarOrigSize has not been set yet, match it with the healthBar rectTransform size
        //if (healthBarOrigSize == Vector2.zero) healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        //GameManager.Instance.coinsText.text = coinsCollected.ToString();
        //GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)playerMaxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }

    //Player can die
    public void Die()
    {
        SceneManager.LoadScene("Level1");
    }

    //Add Inventory Items
    public void AddInventoryItem(string inventoryName, Sprite image = null)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventoryImage.sprite = inventory[inventoryName];
    }

    //Remove Inventory Items
    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        GameManager.Instance.inventoryImage.sprite = inventoryItemBlank;
    }
    
    //Spawn Position on Load Scene
    public void SetSpawnPosition()
    {
        //transform.position = GameObject.Find("SpawnLocation").transform.position;
    }
}


