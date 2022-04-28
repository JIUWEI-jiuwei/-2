using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Music_control : MonoBehaviour
{
    //public AudioSource music;
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    public float v;
    public float v2;
    public float v3;
    // Start is called before the first frame update
    private void Start()
    {
        //music.volume = PlayerPrefs.GetFloat("Volume");
        //slider.value = music.volume;
        
        audioMixer.GetFloat("masterV",out v);
        audioMixer.GetFloat("musicV",out v2);
        audioMixer.GetFloat("SFXV",out v3);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Volume()
    {
        //music.volume = slider.value;
        // PlayerPrefs.SetFloat("Volume", slider.value);
        v = 100*masterSlider.value-80;
        v2 = 100*musicSlider.value-80;
        v3 = 100*SFXSlider.value-80;

        audioMixer.SetFloat("masterV", v);
        audioMixer.SetFloat("musicV", v2);
        audioMixer.SetFloat("SFXV", v3);

    }
}
