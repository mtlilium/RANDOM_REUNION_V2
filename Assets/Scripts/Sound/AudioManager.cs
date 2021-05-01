using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System.Threading.Tasks;
using UniRx;

public class AudioManager : MonoBehaviour
{
    // 出力先
    public AudioMixer audioMixer;

    // SoundsSheet : 情報が入ってる
    private SoundsSheet soundSheet;

    // Sounds の 辞書 <key:string, List<AudioClip>>
    private Dictionary<string, List<AudioClip>> bgmDic;
    private Dictionary<string, List<AudioClip>> seDic;

    private const string BGM_PATH = "Sounds/BGM";
    private const string SE_PATH = "Sounds/SE";
    private const int BGM_SOURCE_NUM = 1;
    private const int SE_SOURCE_NUM = 30;

    private Vector3 BGM_POS = new Vector3(0, 0, 0);

    // AudioMixer の各ミキサーのボリュームとは違う
    private const float BGM_VOLUME = 0.8f;
    private const float SE_VOLUME = 0.7f;

    // BGMは一つづつ鳴るが、SEは複数同時に鳴ることがある
    private AudioSource bgmSource;

    // BGM の　フェードアウトのスピード
    private const float BGM_FADE_OUT_RATE = 0.5f;

    //次流すBGM名
    private string nextBGMName;

    //流れているBGM曲の長さ
    public float nowBGMLength;

    //BGMをフェードアウト中か
    private bool isFadeOut = false;


    private const string SOUND_OBJECT_NAME = "AudioManager";
    private static AudioManager singletonInstance = null;

    private GameObject speakerPrefab;


    public static AudioManager instance
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

    //ゲーム起動時に呼ばれます.
    [RuntimeInitializeOnLoadMethod()]
    static void LoadSounds()
    {
        AudioManager.instance.bgmDic = new Dictionary<string, List<AudioClip>>();
        AudioManager.instance.seDic = new Dictionary<string, List<AudioClip>>();

        AudioManager.instance.soundSheet = Resources.Load("Sounds/Sounds") as SoundsSheet;

        for (int i = 0; i < AudioManager.instance.soundSheet.sheets.Count; i++)
        {
            string dirName = AudioManager.instance.soundSheet.sheets[i].name;
            Debug.Log(dirName + " is now Loading...");
            for (int j = 0; j < AudioManager.instance.soundSheet.sheets[i].list.Count; j++)
            {
                string key = AudioManager.instance.soundSheet.sheets[i].list[j].key;
                Debug.Log("Sounds/" + dirName + "/" + key);
                object[] sounds = Resources.LoadAll("Sounds/" + dirName + "/" + key);
                if (sounds == null)
                {
                    Debug.Log("ERRORRRR");
                }
                List<AudioClip> tmpList = new List<AudioClip>();
                foreach (object sound in sounds)
                {
                    tmpList.Add((AudioClip)sound);
                }
                if (dirName == "BGM")
                {
                    AudioManager.instance.bgmDic.Add(key, tmpList);
                }
                else if (dirName == "SE")
                {
                    AudioManager.instance.seDic.Add(key, tmpList);
                }
            }
        }

        AudioManager.instance.transform.position = new Vector3(0, 0, 0);

        AudioManager.instance.audioMixer = Resources.Load("Sounds/AudioMixer") as AudioMixer;

        AudioManager.instance.speakerPrefab = (GameObject)Resources.Load("Sounds/Speaker");

        AudioManager.instance.bgmSource = AudioManager.instance.gameObject.AddComponent<AudioSource>();
        AudioManager.instance.bgmSource.playOnAwake = false;
        AudioManager.instance.bgmSource.loop = true;
        AudioManager.instance.bgmSource.spatialBlend = 0.0f;
        AudioManager.instance.bgmSource.outputAudioMixerGroup = AudioManager.instance.audioMixer.FindMatchingGroups("BGM")[0];

        for (int i = 0; i < SE_SOURCE_NUM + BGM_SOURCE_NUM; i++)
        {
            //Speaker Prefab生成
            AudioManager.instance.CreateSpeaker(AudioManager.instance.speakerPrefab);
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    /// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// 第二引数のfadeOutRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
    public void PlayBGM(string key, float vol = BGM_VOLUME, float fadeOutRate = BGM_FADE_OUT_RATE)
    {
        if (!bgmDic.ContainsKey(key))
        {
            Debug.Log(key + "という名前のBGMがありません");
            return;
        }

        //現在BGMが流れていない時はそのまま流す
        if (!bgmSource.isPlaying)
        {
            nextBGMName = "";
            bgmSource.clip = bgmDic[key][0];
            bgmSource.volume = vol;
            bgmSource.Play();
            nowBGMLength = bgmSource.clip.length;
        }
        //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
        else if (bgmSource.clip.name != key)
        {
            nextBGMName = key;
            isFadeOut = true;
        }

    }
    private void Update()
    {
        if (!isFadeOut)
        {
            return;
        }

        //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
        bgmSource.volume -= Time.deltaTime * BGM_FADE_OUT_RATE;
        if (bgmSource.volume <= 0)
        {
            bgmSource.Stop();
            bgmSource.volume = BGM_VOLUME;
            isFadeOut = false;

            if (!string.IsNullOrEmpty(nextBGMName))
            {
                PlayBGM(nextBGMName);
            }
        }

    }

    // 重複して同じ音が鳴るとき、一気にSpeakerが占領されるのでInstanceIDとSpeakerを紐づけて, history で管理
    // 紐づけがあれば同じSpeakerで鳴らしなおすことができる.
    public float PlaySE(string key, GameObject obj, float vol = SE_VOLUME, float delay = 0.0f, bool isLoop = false)
    {

        Speaker speaker = FindEmptySpeaker().GetComponent<Speaker>();

        if (speaker == null)
        {
            speaker = CreateSpeaker(speakerPrefab).GetComponent<Speaker>();
        }
        int seLength = seDic[key].Count;
        AudioClip seClip = seDic[key][(int)Random.Range(0, seLength)];

        speaker.PlayAudio(seClip, obj, vol, delay);

        return seClip.length;
    }

    public float PlaySE(string key, Vector3 pos, float vol = SE_VOLUME, float delay = 0.0f, bool isLoop = false)
    {

        Speaker speaker = FindEmptySpeaker().GetComponent<Speaker>();

        if (speaker == null)
        {
            speaker = CreateSpeaker(speakerPrefab).GetComponent<Speaker>();
        }
        int seLength = seDic[key].Count;
        AudioClip seClip = seDic[key][(int)Random.Range(0, seLength)];

        speaker.PlayAudio(seClip, pos, vol, delay);
        return seClip.length;
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
