using UnityEngine;
using System.Collections;

public class flickerLightBulb : MonoBehaviour {
	
	Light target;
	
	[Range(0,2)]public float minValue;
	[Range(0,2)]public float maxValue;
	float range;
	public float speedScale = 7f;

	bool flicker = true;

	float nextTime;
	float delay = Random.Range (3f,8f);

	// Use this for initialization
	void Start () {
		target = GetComponent<Light>();
		range = (maxValue - minValue);
	}
	
	// Update is called once per frame
	void Update () {
		
		float waveValue = Mathf.PerlinNoise (Time.time * speedScale, 0f);
		
		float value = waveValue * range;
		value += minValue;

		if (Time.time > nextTime) {
			flicker = !flicker;
			nextTime = Time.time + delay;
		}

		if (flicker == true) {
						target.intensity = value;
				} else {
						target.intensity = 0.8f;
				}
	}
}
