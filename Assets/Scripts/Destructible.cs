using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Destructible : MonoBehaviour
{
    [SerializeField] int hitToDestroy = 3;
    [SerializeField] AudioClip[] hitClips;
    [SerializeField] AudioClip[] destroyClips;
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
            AudioManager.instance.PlayAudioClip(hitClips[Random.Range(0, hitClips.Length)], transform.position);
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect() 
    {
        spriteRenderer.color = Color.red;
        transform.localScale = Vector3.one * 1.2f;

        yield return new WaitForSeconds(0.075f);

        spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;
    }

    void DestroyThis()
    {
        if (Random.Range(0f, 100f) <= Gamemanager.instance.MissionPickableDropsChance)
        {
            GameObject pickableObject = Gamemanager.instance.GetRandomPickable(pickableObjectType.Mission).gameObject;

            if (pickableObject != null)
            {
                Instantiate(pickableObject, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
            }
        }
        else if (Random.Range(0f, 100f) <= Gamemanager.instance.PowerUpPickableDropsChance)
        {
            Instantiate(Gamemanager.instance.GetRandomPickable(pickableObjectType.PowerUp).gameObject, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        }

        AudioManager.instance.PlayAudioClip(destroyClips[Random.Range(0, destroyClips.Length)], transform.position);

        StopAllCoroutines();
        visualEffect.transform.parent = null;
        visualEffect.Play();
        Destroy(visualEffect, 3);
        Destroy(gameObject);
    }
}
