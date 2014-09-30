using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerVisibility : MonoBehaviour
{
	public bool UseChildren;

	[Range(0, 1)] public float VisibilityChance; //Chance every second
	public float VisibilityLenght;//The amount of time the object is visible in seconds

	public bool UseFade;
	[Range(0, 1)] public float FadeSpeed;//As lerp time

	public float MaximumVisibility;
	public float MinimumVisibility;

	public bool UseRandomVisibility;

	float TimeSinceTick;//The tick tracker
	List<Renderer> Renderers = new List<Renderer>();
	float OldVisibility;
	bool IsFading;

	void Start ()
	{
		//If UseChildren is true, we want to process them too. So we add them to the list.
		if (UseChildren)
		{
			Renderers = transform.GetComponentsInChildren<Renderer>().ToList();
		}

		//Of course we need to add ourselve too.
		if (renderer != null)
		{
			Renderers.Add(renderer);
		}
	}
	
	void FixedUpdate ()
	{
		//Add the fixed time to the tracker.
		TimeSinceTick += Time.fixedDeltaTime;

		//If the tracker has a value of higher than 1f, then one second has passed. Do a tick.
		if (TimeSinceTick >= 1)
		{
			//Reset the tracker
			TimeSinceTick = 0;

			//Calculate if this tick the objects should become visible.
			if (Random.Range(0f, 1f) < VisibilityChance)
			{
				if (!UseRandomVisibility)
				{
					StartCoroutine(FadeToVisibility(MaximumVisibility, 0));
				}
				else
				{
					StartCoroutine(FadeToVisibility(Random.Range(MinimumVisibility, MaximumVisibility), 0));
				}
				StartCoroutine(FadeToVisibility(MinimumVisibility, VisibilityLenght));
			}
		}
	}

	IEnumerator FadeToVisibility(float Value, float Delay)
	{
		//Yield the Delay.
		yield return new WaitForSeconds(Delay);

		//If another IEnumerator is fading, we don't want to start ourselves.
		while (IsFading)
		{
			yield return new WaitForFixedUpdate();
		}

		IsFading = true;

		OldVisibility = Visibility;
		//This will run until the difference between the Old Visibility and the current is less than 0.01f.
		while (Mathf.Abs(OldVisibility - Value) < 0.01f)
		{
			Visibility = Mathf.Lerp(OldVisibility, Value, FadeSpeed);
			//Wait for the next fixed update.
			yield return new WaitForFixedUpdate();
		}
		//Make sure there is no diffence of 0.01f.
		Visibility = Value;

		IsFading = false;
	}

	float _Visibility;
	float Visibility
	{
		set
		{
			float Value = value;

			//Clamp the value between 0 and 1.
			if (value > 1f || value < 0f)
			{
				Value = Mathf.Clamp(value, 0f, 1f);
			}

			_Visibility = Value;

			foreach (Renderer Render in Renderers)
			{
				if (!UseFade)
				{
					if (Value > 0.5f)
					{
						Render.enabled = true;
					}
					else
					{
						Render.enabled = false;
					}
				}
				else
				{
					Render.enabled = true;
					Color Colour = Render.material.color;
					Colour.a = Value;
					Render.material.color = Colour;
				}
			}
		}
		get
		{
			return _Visibility;
		}
	}
}
