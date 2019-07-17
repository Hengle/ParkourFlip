using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using Facebook.Unity;
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
	void Start () 
   {
        FB.Init(OnFacebookInitialize);
	}

    void OnFacebookInitialize()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
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
