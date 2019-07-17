using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private readonly  Vector3 _distance = new Vector3(0,1.7f,0);
    public Rigidbody rb;
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.Instance.transform.position - _distance ;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Combo")
        {
            if (!Player.Instance.isDead)
            {
                StartCoroutine(UIManager.Instance.ShowComboText());    
                GameManager.Instance.combo += 1;
            }
        }
    }
    
    
}
