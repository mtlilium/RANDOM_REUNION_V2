using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Speaker : MonoBehaviour
{

    AudioSource audioSource;
    public bool isUsed;

    // Start is called before the first frame update
    void Awake()
    {
        isUsed = false;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator PlayAudio(Vector3 pos, AudioClip audioClip, AudioMixerGroup outputMixer, float delay = 0.0f, float volume=0.5f, bool isLoop = false, bool isBGM = false)
    {
        yield return new WaitForSeconds(delay);


        
        transform.position = pos;

        audioSource.outputAudioMixerGroup = outputMixer;
        audioSource.volume = volume;

        audioSource.clip = audioClip;

        if (isLoop)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }

        if (isBGM)
        {
            audioSource.spatialBlend = 0.0f;
        }
        else
        {
            audioSource.spatialBlend = 1.0f;
        }

        audioSource.Play();
    }

    public IEnumerator RePlayAudio(Vector3 pos, AudioClip audioClip, AudioMixerGroup outputMixer, float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);

        transform.position = pos;
        audioSource.clip = audioClip;
        audioSource.outputAudioMixerGroup = outputMixer;

        audioSource.Play();
    }

    public bool IsEmpty()
    {
        return audioSource.time == 0.0f && !audioSource.isPlaying;
        //Play()した直後にPause()すると、timeは0だしisPlayingはfalseなので再生終了したと判定されてしまうが,
        //Pauseせず再生状態のままピッチを0.0fにすればいいです.
    }
}
