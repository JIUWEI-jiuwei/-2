using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MixerController2 : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float v;
        audioMixer.GetFloat("Volume", out v);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioMixer.SetFloat("Volume", v + 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioMixer.SetFloat("Volume", v - 1);
        }
    }
}
