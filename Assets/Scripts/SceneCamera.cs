using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneCamera : MonoBehaviour
{

    [Header("References")]
   [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;


    // Start is called before the first frame update
    void Start()
    {
        //set cinemachineVirtualCamera.Target = NewPlayer.Instance
        //cinemachineVirtualCamera.Follow = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
