using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour {

	public Animator anim;
	public string sceneName;
	private bool isKeyPress = false;

	private void Awake()
	{
		GameAnalytics.Initialize();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene()
	{
		anim.SetBool("IsCliked" , true);
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(sceneName);

	
	}

}
