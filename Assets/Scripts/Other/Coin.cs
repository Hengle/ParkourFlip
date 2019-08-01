using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class Coin : MonoBehaviour
{

    private float speed;
    private GameObject _particleEffect;
    private MeshRenderer _renderer;
    private bool canTake = true;
    private Vector3 startPoisition;
    private Player player;
    private void Start()
    {
        speed = Random.Range(100, 250);
        _renderer = GetComponent<MeshRenderer>();
        
        player=Player.Instance;

        startPoisition = transform.position;
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance < 5 && player.riskyJump)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 10);
        }
        
        transform.Rotate(0,speed * Time.deltaTime,0,Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && canTake)
        {
            canTake = false;
            GameManager.Instance.coinCount += 1;
            CollectionManager.Instance.collectCoin();
            _renderer.enabled = false;
            StartCoroutine("Effect");
            
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(3f);
        canTake = true;
        _renderer.enabled = true;
        transform.position = startPoisition;
    }
    private IEnumerator Effect()
    {
        _particleEffect = ObjectPooler.SharedInstance.GetPooledObject(5);
        _particleEffect.transform.position = gameObject.transform.position;
        _particleEffect.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _particleEffect.SetActive(false);
        StartCoroutine(Reset());
    }
}
