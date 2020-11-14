using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    // 出力先
    public AudioMixer audioMixer;

    private const string BGM_PATH = "Sounds/BGM";
    private const string SE_PATH = "Sounds/SE";
    private const int BGM_SOURCE_NUM = 1;
    private const int SE_SOURCE_NUM = 10;

    private Vector3 BGM_POS = new Vector3(0, 0, 0);

    // AudioMixer の各ミキサーのボリュームとは違う
    private const float BGM_VOLUME = 0.5f;
    private const float SE_VOLUME = 0.3f;

    // BGMは一つづつ鳴るが、SEは複数同時に鳴ることがある
    private AudioSource bgmSource;
    private List<AudioSource> seSourceList;
    private Dictionary<string, AudioClip> seClipDic;
    private Dictionary<string, AudioClip> bgmClipDic;
    private Dictionary<int, Speaker> historySE;
    private Dictionary<int, Speaker> historyBGM;

    private const string SOUND_OBJECT_NAME = "AudioManager";
    private static AudioManager singletonInstance = null;

    private GameObject speakerPrefab;


    public static AudioManager SingletonInstance
    {
        get
        {
            if (!singletonInstance)
            {
                GameObject obj = new GameObject(SOUND_OBJECT_NAME);
                singletonInstance = obj.AddComponent<AudioManager>();
                DontDestroyOnLoad(obj);
                obj.transform.position = new Vector3(0, 0, 0);
            }
            return singletonInstance;
        }
    }


    private void Awake()
    {

        transform.position = new Vector3(0, 0, 0);

        audioMixer = Resources.Load("AudioMixer/AudioMixer") as AudioMixer;

        speakerPrefab = (GameObject)Resources.Load("Speaker");

        for (int i = 0; i < SE_SOURCE_NUM + BGM_SOURCE_NUM; i++)
        {
            //Speaker Prefab生成
            CreateSpeaker(speakerPrefab);
        }

        bgmClipDic = (Resources.LoadAll(BGM_PATH) as Object[]).ToDictionary(bgm => bgm.name, bgm => (AudioClip)bgm);
        seClipDic = (Resources.LoadAll(SE_PATH) as Object[]).ToDictionary(se => se.name, se => (AudioClip)se);

        historySE = new Dictionary<int, Speaker>();
        historyBGM = new Dictionary<int, Speaker>();
    }


    // 重複して同じ音が鳴るとき、一気にSpeakerが占領されるのでInstanceIDとSpeakerを紐づけて, history で管理
    // 紐づけがあれば同じSpeakerで鳴らしなおすことができる.
    public void RequestPlaySE(Vector3 pos, string fileName, int instanceID, float delay = 0.0f, bool isLoop = false, bool isEvent = false)
    {
        Speaker speaker;

        string outMixerName = "SE";

        if (isEvent)
        {
            outMixerName += "_Event";
        }

        if (historySE.ContainsKey(instanceID))
        {
            speaker = historySE[instanceID];

        }
        else
        {
            speaker = FindEmptySpeaker().GetComponent<Speaker>();

            if (speaker == null)
            {
                speaker = CreateSpeaker(speakerPrefab).GetComponent<Speaker>();
            }

            historySE = new Dictionary<int, Speaker> { { instanceID, speaker } };

        }

        StartCoroutine(speaker.PlayAudio(pos, seClipDic[fileName], audioMixer.FindMatchingGroups(outMixerName)[0], delay, SE_VOLUME, isLoop, false));
    }

    public void RequestPlaySE(Vector3 pos, AudioClip audioClip, int instanceID, float delay = 0.0f, bool isLoop = false, bool isEvent = false)
    {
        Speaker speaker;

        string outMixerName = "SE";

        if (isEvent)
        {
            outMixerName += "_Event";
        }

        if (historySE.ContainsKey(instanceID))
        {
            speaker = historySE[instanceID];

        }
        else
        {
            speaker = FindEmptySpeaker().GetComponent<Speaker>();

            if (speaker == null)
            {
                speaker = CreateSpeaker(speakerPrefab).GetComponent<Speaker>();
            }

            historySE = new Dictionary<int, Speaker> { { instanceID, speaker } };

        }

        StartCoroutine(speaker.PlayAudio(pos, audioClip, audioMixer.FindMatchingGroups(outMixerName)[0], delay, SE_VOLUME, isLoop, false));
    }

    public void RequestPlayBGM(string fileName, int instanceID, float delay = 0.0f, bool isLoop = true)
    {
        Speaker speaker;
        if (historyBGM.ContainsKey(instanceID))
        {
            speaker = historyBGM[instanceID];
        }
        else
        {
            speaker = FindEmptySpeaker().GetComponent<Speaker>();

            if (speaker == null)
            {
                speaker = CreateSpeaker(speakerPrefab).GetComponent<Speaker>();
            }

            historyBGM = new Dictionary<int, Speaker> { { instanceID, speaker } };

        }

        StartCoroutine(speaker.PlayAudio(BGM_POS, bgmClipDic[fileName], audioMixer.FindMatchingGroups("BGM")[0], delay, BGM_VOLUME, isLoop, true));
    }

    public void RequestPlayBGM(AudioClip audioClip, int instanceID, float delay = 0.0f, bool isLoop = true)
    {
        Speaker speaker;
        if (historyBGM.ContainsKey(instanceID))
        {
            speaker = historyBGM[instanceID];
        }
        else
        {
            speaker = FindEmptySpeaker().GetComponent<Speaker>();

            if (speaker == null)
            {
                speaker = CreateSpeaker(speakerPrefab).GetComponent<Speaker>();
            }

            historyBGM = new Dictionary<int, Speaker> { { instanceID, speaker } };

            
        }

        StartCoroutine(speaker.PlayAudio(BGM_POS, audioClip, audioMixer.FindMatchingGroups("BGM")[0], delay, BGM_VOLUME, isLoop, true));
    }


    private GameObject FindEmptySpeaker()
    {
        GameObject target = null;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Speaker>().IsEmpty() == true)
            {
                target = child.gameObject;
                break;
            }
        }

        return target;
    }


    private GameObject CreateSpeaker(GameObject prefab)
    {
        //Speaker Prefab生成

        GameObject obj = (GameObject)Instantiate(speakerPrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = BGM_POS;

        return obj;
    }
}
