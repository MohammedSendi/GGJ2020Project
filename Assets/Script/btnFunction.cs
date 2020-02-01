using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFunction : MonoBehaviour
{

	public int points;

	public void BTN1Click()
	{
		GameObject.FindGameObjectsWithTag("Loader")[0].GetComponent<Loader>().checkAnswer(1);
	}
	public void BTN2Click()
	{
		GameObject.FindGameObjectsWithTag("Loader")[0].GetComponent<Loader>().checkAnswer(2);
	}
}
