using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    
    #region Singleton

    public static ComboController Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
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
    
    private readonly  Vector3 _distance = new Vector3(0,1.7f,0);
    private Rigidbody _rb;
    public bool canFlip = true;

    private float _currentRot;
    private float _afterRot;

    private float result;
    // Update is called once per frame

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.position = Player.Instance.transform.position - _distance ;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Combo" && canFlip)
        {
            if (!Player.Instance.isDead)
            {
                StartCoroutine(UIManager.Instance.ShowComboText());
                StartCoroutine(Flip());
            }
        }
    }


    private IEnumerator Flip()
    {
        CollectionManager.Instance.flip();
        _currentRot = Player.Instance.transform.eulerAngles.z;
        canFlip = false;
        GameManager.Instance.combo += 1;
        Taptic.Medium();
        yield return new WaitForSeconds(0.1f);
        _afterRot = Player.Instance.transform.eulerAngles.z;
        result =  _currentRot - _afterRot;
        if (result <= 30 )
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }
    }
}
