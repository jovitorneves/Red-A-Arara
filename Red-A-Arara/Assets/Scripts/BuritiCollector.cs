using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuritiCollector : MonoBehaviour
{

    public AudioClip fxCollect;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            GameManager.instance.score++;
            SoundManager.instance.PlayFxBuritiCollector(fxCollect);
            Destroy(gameObject);

        }
    }
}
