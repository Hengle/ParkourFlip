using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    #region Singleton

    public static ParticleManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    private Vector3 starDifference = new Vector3(0f, 5f, 0f);
    private Vector3 dif = new Vector3(1.5f, 0f, 2f);
    private Vector3 trail = new Vector3(5f, 0, 0f);
    

    // ParticleEffect
    private GameObject trailEffect;
    private GameObject DeathEffect;
    private GameObject GameEndEffect;
    
    private void Start()
    {
        //trailEffect = ObjectPooler.SharedInstance.GetPooledObject(5);
        GameEndEffect = ObjectPooler.SharedInstance.GetPooledObject(3);
        DeathEffect = ObjectPooler.SharedInstance.GetPooledObject(2);
    }

    private void Update()
    {
       /* if (Player.Instance._isGrounded)
        {
            trailEffect.SetActive(true);
            trailEffect.transform.position = Player.Instance.transform.position - trail ;
        }
        else
        {
            trailEffect.SetActive(false);
        }*/
    }
   
    public IEnumerator JumpingEffects(GameObject jumpingEffect)
    {
        jumpingEffect = ObjectPooler.SharedInstance.GetPooledObject(0);
        jumpingEffect.transform.position = Player.Instance.transform.position;
        jumpingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        jumpingEffect.SetActive(false);
    }
    
    public IEnumerator StarEffects(GameObject star)
    {
        star = ObjectPooler.SharedInstance.GetPooledObject(4);
        star.transform.position = Player.Instance.transform.position + starDifference ;
        star.SetActive(true);
        yield return new WaitForSeconds(1f);
        star.SetActive(false);
    }
    public IEnumerator GameEndEffects()
    {
        GameEndEffect.transform.position = Player.Instance.transform.position + dif;
        GameEndEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameEndEffect.SetActive(false);
    }
    
    public IEnumerator LandingEffects(GameObject landingEffect)
    {
        
        landingEffect = ObjectPooler.SharedInstance.GetPooledObject(1);
        landingEffect.transform.position = Player.Instance.transform.position;
        landingEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        landingEffect.SetActive(false);
    }
    
    public IEnumerator DeathEffects()
    {
        DeathEffect.transform.position = Player.Instance.transform.position;
        DeathEffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        DeathEffect.SetActive(false);
    }
    
}
