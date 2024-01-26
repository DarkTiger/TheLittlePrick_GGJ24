using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Destructible : MonoBehaviour
{
    [SerializeField] int hitToDestroy = 3;
    SpriteRenderer spriteRenderer;
    VisualEffect visualEffect;

    int health = -1;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        visualEffect = GetComponentInChildren<VisualEffect>();
        
        health = hitToDestroy;
    }

    private void Start()
    {
        visualEffect.Stop();
    }

    public void GetHitDamage()
    {
        health--;

        if (health <= 0)
        {
            DestroyThis();
        }
        else
        {
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect() 
    {
        spriteRenderer.color = Color.red;
        transform.localScale = Vector3.one * 1.1f;

        yield return new WaitForSeconds(0.05f);

        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }

    void DestroyThis()
    {
        StopAllCoroutines();
        visualEffect.transform.parent = null;
        visualEffect.Play();
        Destroy(visualEffect, 3);
        Destroy(gameObject);
        //generate item
    }
}
