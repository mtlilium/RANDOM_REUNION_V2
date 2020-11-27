using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Synthesizer : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    Slider dampingSliderOn;
    [SerializeField]
    Slider dampingSliderOff;

    const float outputSampleRate = 44100.0f;
	const float strength = 0.15f;

	private float deltaTime;
    bool isRunning;
	struct Oscillator
	{
		public void Attack(float strength)
		{
			var pulse = strength * Mathf.Sqrt(stiffness);
			velocity += pulse;
		}
		public float WaveUpdate(float deltaTime)
		{
			velocity -= velocity * damping * deltaTime;
			velocity -= position * stiffness * deltaTime;
			position += velocity * deltaTime;
			return position;
		}
		public float position;
		float velocity;
		public float stiffness { set; private get; }
		public float damping { set; private get; }
	}
	Oscillator[] oscillators;

	private KeyCode[] keys = new KeyCode[]
	{
			KeyCode.A,
			KeyCode.W,
			KeyCode.S,
			KeyCode.E,
			KeyCode.D,
			KeyCode.F,
			KeyCode.T,
			KeyCode.G,
			KeyCode.Y,
			KeyCode.H,
			KeyCode.U,
			KeyCode.J,
			KeyCode.K,
			KeyCode.O,
			KeyCode.L,
			KeyCode.P,
			KeyCode.Semicolon,
			KeyCode.Colon
	};

	void Start()
    {
        deltaTime = 1f / outputSampleRate;
		isRunning = true;
        oscillators = new Oscillator[20];
        var step = Mathf.Pow(2f, 1f / 12f);
        var f = 261.6255653005985f;
        for (int i = 0; i < oscillators.Length; i++)
        {
            var w = f * 2f * Mathf.PI;
            var stiffness = w * w;
            oscillators[i].stiffness = stiffness;
            oscillators[i].damping = 1f;
            f *= step;
        }
    }

	void Update()
	{
		var dampingOn = Mathf.Pow(10f, dampingSliderOn.value);
		var dampingOff = Mathf.Pow(10f, dampingSliderOff.value);
		
		var on = new bool[oscillators.Length];
		for (int i = 0; i < keys.Length; i++)
		{
			var note = i;
			if (Input.GetKeyDown(keys[i]))
			{
				Attack(note, strength);
			}
			if (Input.GetKey(keys[i]))
			{
				on[note] = true;
			}
		}

		for (int i = 0; i < oscillators.Length; i++)
		{
			var d = on[i] ? dampingOn : dampingOff;
			oscillators[i].damping = d;
		}
	}

	void Attack(int index, float strength)
	{
		Debug.Log(index);
		oscillators[index].Attack(strength);
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		if (!isRunning)
		{
			return;
		}

		int sampleCount = data.Length / channels;
		for (int i = 0; i < sampleCount; i++)
		{
			float v = UpdateOsccilators();
			for (int c = 0; c < channels; c++)
			{
				data[(i * channels) + c] = v;
			}
		}
	}

	float UpdateOsccilators()
	{
		var v = 0f;
		for (int i = 0; i < oscillators.Length; i++)
		{
			v += oscillators[i].WaveUpdate(deltaTime);
		}
		return v;
	}
}
