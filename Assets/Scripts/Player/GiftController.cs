using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftController : MonoBehaviour
{
    public bool isGift;
    public Transform chimneyTarget;
    public TextMeshPro chimneytext;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isGift = true;
        }
    }
}
