using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
{
    public AudioSource openDoor;
    public AudioSource closeDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            openDoor.Play();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            closeDoor.Play();
        }
    }
}
