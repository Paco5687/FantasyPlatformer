using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text coinsText;
    public Image healthBar;
    public Image inventoryImage;

    //Singleton instantiation
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    // Awake is called before the Start function
    private void Awake()
    {
        if (GameObject.Find("NewGameManager")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "NewGameManager";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
