using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!Player.Instance.isDead)
            {
                GameManager.Instance.gameEnd = true;
                Player.Instance.isWin = true;
                StartCoroutine(ParticleManager.Instance.GameEndEffects());
            }
        }
    }
}
