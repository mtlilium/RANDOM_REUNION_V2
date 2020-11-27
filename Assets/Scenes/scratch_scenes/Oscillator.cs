using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Oscillator
{
	private float position;
	private float velocity;
	public float stiffness { set; private get; }
	public float damping { set; private get; }

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
	//const double PI = System.Math.PI;
	//public enum PlayState
	//{
	//	None,
	//	C,
	//	CS,
	//	D,
	//	DS,
	//	E,
	//	F,
	//	FS,
	//	G,
	//	GS,
	//	A,
	//	AS,
	//	B,
	//	C2
	//}
	//public double freq = 440.0f;
	//private double increment;
	//private double sampling_freq = 48000.0f;
	//private double phase;
	//public float gain;
	//public float volume = 0.1f;
	//public int thisFreq;
	//public Dictionary<PlayState, double> keyFreqDic = new Dictionary<PlayState, double>()
	//{
	//	{PlayState.C, 261.6255653005985},
	//	{PlayState.CS, 277.18263097687196},
	//	{PlayState.D, 293.66476791740746},
	//	{PlayState.DS, 311.1269837220808},
	//	{PlayState.E, 329.62755691286986},
	//	{PlayState.F, 349.2282314330038},
	//	{PlayState.FS, 369.99442271163434},
	//	{PlayState.G, 391.99543598174927},
	//	{PlayState.GS, 415.3046975799451},
	//	{PlayState.A, 440.0},
	//	{PlayState.AS, 466.1637615180899},
	//	{PlayState.B, 493.8833012561241},
	//	{PlayState.C2, 523.2511306011974}
	//};
	//// Start is called before the first frame update
	//void Start()
	//   {

	//   }

	//   // Update is called once per frame
	//   void Update()
	//   {
	//       if (Input.GetKeyDown(KeyCode.Space))
	//       {
	//		gain = volume;

	//       }
	//       if (Input.GetKeyUp(KeyCode.Space))
	//       {
	//		gain = 0.0f;
	//       }
	//	//if (Input.GetKey("z"))
	//	//{
	//	//	Play(PlayState.C);
	//	//}
	//	//if (Input.GetKey("s"))
	//	//{
	//	//	Play(PlayState.CS);
	//	//}
	//	//if (Input.GetKey("x"))
	//	//{
	//	//	Play(PlayState.D);
	//	//}
	//	//if (Input.GetKey("d"))
	//	//{
	//	//	Play(PlayState.DS);
	//	//}
	//	//if (Input.GetKey("c"))
	//	//{
	//	//	Play(PlayState.E);
	//	//}
	//	//if (Input.GetKey("v"))
	//	//{
	//	//	Play(PlayState.F);
	//	//}
	//	//if (Input.GetKey("g"))
	//	//{
	//	//	Play(PlayState.FS);
	//	//}
	//	//if (Input.GetKey("b"))
	//	//{
	//	//	Play(PlayState.G);
	//	//}
	//	//if (Input.GetKey("h"))
	//	//{
	//	//	Play(PlayState.GS);
	//	//}
	//	//if (Input.GetKey("n"))
	//	//{
	//	//	Play(PlayState.A);
	//	//}
	//	//if (Input.GetKey("j"))
	//	//{
	//	//	Play(PlayState.AS);
	//	//}
	//	//if (Input.GetKey("m"))
	//	//{
	//	//	Play(PlayState.B);
	//	//}
	//	//if (Input.GetKey(","))
	//	//{
	//	//	Play(PlayState.C2);
	//	//}
	//}

	//void OnAudioFilterRead(float[] data, int channels)
	//   {
	//	increment = freq * 2.0 * PI / sampling_freq;

	//	for(int i = 0; i < data.Length; i += channels)
	//       {
	//		phase += increment;
	//		data[i] = (float)(gain * Mathf.Sin((float)phase));

	//		if(channels == 2)
	//           {
	//			data[i + 1] = data[i];
	//           }
	//		if(phase > (PI * 2))
	//           {
	//			phase = 0.0f;
	//           }
	//       }
	//   }
}
