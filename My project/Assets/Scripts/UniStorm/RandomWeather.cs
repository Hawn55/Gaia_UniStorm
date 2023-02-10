using UnityEngine;
using System.Collections;
using UniStorm;

public class RandomWeather : MonoBehaviour
{
	[SerializeField]
	private UniStormSystem uniStormSystem;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			uniStormSystem.ChangeWeather(uniStormSystem.AllWeatherTypes[Random.Range(1, 13)]);
		}
	}
}