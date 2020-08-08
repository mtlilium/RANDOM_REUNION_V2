using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSEVol()
    {
        audioMixer.SetFloat("vol_SE", -40f);
    }
}
