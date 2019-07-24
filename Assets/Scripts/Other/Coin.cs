using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Coin : MonoBehaviour
{

    private float speed;
    private GameObject _particleEffect;
    private MeshRenderer _renderer;
    private bool canTake = true;
    private void Start()
    {
        speed = Random.Range(100, 250);
        gameObject.SetActive(true);
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = true;
    }

    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canTake)
        {
            canTake = false;
            GameManager.Instance.coinCount += 5;
            _renderer.enabled = false;
            StartCoroutine("Effect");
        }
    }

    private IEnumerator Effect()
    {
        _particleEffect = ObjectPooler.SharedInstance.GetPooledObject(5);
        _particleEffect.transform.position = gameObject.transform.position;
        _particleEffect.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _particleEffect.SetActive(false);
        gameObject.SetActive(false);
    }
}
