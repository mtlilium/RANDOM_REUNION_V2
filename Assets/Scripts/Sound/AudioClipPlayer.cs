using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> clipList;

    Dictionary<string, AudioClip> clipDict;

    AudioSource source;
    private void Start() {
        source = GetComponent<AudioSource>();
        if (source == null) {
            Debug.Log(gameObject.name+"にAudioSourceがありません");
        }
        clipDict = new Dictionary<string, AudioClip>();
        foreach(var clip in clipList) {
            clipDict.Add(clip.name, clip);
        }
    }
    public void Play(string audioName) {
        if (!clipDict.ContainsKey(audioName)) {
            Debug.Log(audioName + "という名前のAudioClipがclipDict内に見つかりません");
            return;
        }
        source.PlayOneShot(clipDict[audioName]);
    }
}
