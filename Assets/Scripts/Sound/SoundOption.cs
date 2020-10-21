using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
	[SerializeField]
	private AudioMixer audioMixer;

	//SoundOptionキャンバス
	/*
    [SerializeField]
	private GameObject soundOptionCanvas;
    */


	private void Awake()
    {
        /*
        if (soundOptionCanvas.gameObject.activeSelf)
        {
			soundOptionCanvas.SetActive(false);

		}
        */
    }

    void Update()
	{
        /*
		//　3が押されたらUIをオン・オフ
		if (Input.GetKeyDown("3"))
		{
			soundOptionCanvas.SetActive(!soundOptionCanvas.activeSelf);

		}
        */
	}

	public void SetMaster(float volume)
	{
		//-80 to 20
		audioMixer.SetFloat("vol_Master", volume);
	}

	public void SetBGM(float volume)
	{
		//-80 to 20
		audioMixer.SetFloat("vol_BGM", volume);
	}

	public void SetSE(float volume)
	{
		//-80 to 20
		audioMixer.SetFloat("vol_SE", volume);
		audioMixer.SetFloat("vol_SE_Event", volume);
	}
	public void SetSEReverb(float volume)
	{
		//-80 to 0
		audioMixer.SetFloat("Reverb", volume);
	}

	public void SetBGMEffects(float volume)
	{
		// -80 to 0
		audioMixer.SetFloat("BGM_Flanger", volume);
		audioMixer.SetFloat("BGM_Pitch", volume);
	}
}
