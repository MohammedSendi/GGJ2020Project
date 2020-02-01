using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnFunction : MonoBehaviour
{

	public int points;
	public GameObject next1;
	public GameObject next2;


	public void BTN1Click()
	{
		GameObject.FindGameObjectsWithTag("Loader")[0].GetComponent<Loader>().checkAnswer(1);
	}
	public void BTN2Click()
	{
		GameObject.FindGameObjectsWithTag("Loader")[0].GetComponent<Loader>().checkAnswer(2);
	}

	public void nextStage1(){

		SceneManager.LoadScene("dialog scene");
	}
	public void nextStage2(){

		SceneManager.LoadScene("dialog scene 2");
	}
}
