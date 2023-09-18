using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;

    void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity); //Instanciando a explos�o no local que o inimigo morre
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }
}
