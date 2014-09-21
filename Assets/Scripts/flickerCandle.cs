using UnityEngine;
using System.Collections;

public class flickerCandle : MonoBehaviour {

	Light target;

	[Range(0,1)]public float minValue;
	[Range(0,1)]public float maxValue;
	float range;
	public float speedScale = 3f;

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
		
		target.intensity = value;
	}
}
