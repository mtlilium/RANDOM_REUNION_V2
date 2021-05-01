using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Speaker : MonoBehaviour
{

    AudioSource audioSource;
    public bool isUsing;

    // Start is called before the first frame update
    void Awake()
    {
        isUsing = false;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying & isUsing)
        {
            GetBack();
        }
    }

    public void PlayAudio(AudioClip audioClip, GameObject obj, float vol, float delay = 0.0f)
    {
        //yield return new WaitForSeconds(delay);

        //this.transform.parent = AudioManager.instance.gameObject.transform;
        this.transform.SetParent(obj.transform, false);
        this.transform.localPosition = new Vector3(0, 0, 0);
        isUsing = true;
        audioSource.volume = vol;
        audioSource.clip = audioClip;

        audioSource.Play();
    }

    public void PlayAudio(AudioClip audioClip, Vector3 pos, float vol, float delay = 0.0f)
    {
        //yield return new WaitForSeconds(delay);

        //this.transform.parent = AudioManager.instance.gameObject.transform;
        this.transform.localPosition = pos;
        isUsing = true;
        audioSource.volume = vol;
        audioSource.clip = audioClip;

        audioSource.Play();
    }

    public bool IsEmpty()
    {
        return audioSource.time == 0.0f && !audioSource.isPlaying;
        //Play()した直後にPause()すると、timeは0だしisPlayingはfalseなので再生終了したと判定されてしまうが,
        //Pauseせず再生状態のままピッチを0.0fにすればいいです.
    }

    private void GetBack()
    {
        isUsing = false;
        this.transform.SetParent(AudioManager.instance.gameObject.transform, false);
        this.transform.localPosition = new Vector3(0, 0, 0);
    }
}
