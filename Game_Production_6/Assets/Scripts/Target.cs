using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Target_Types target_type;
    [SerializeField] ParticleSystem Ex_vfx; 
    [SerializeField] ParticleSystem FB_vfx; 
    [SerializeField] GameObject FB_Effect; 
    [SerializeField] float Fuse; // 2

    Player player;
    enum Target_Types
    {
        Still,
        Moving,
        Ex,
        FB,
        Shoot,
        Tank
    }
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (target_type == Target_Types.Ex)
        {
            Ex_vfx = GameObject.Find("Ex_Vfx").GetComponent<ParticleSystem>();
            //Ex_vfx.Stop();
        }
    }

    void Update()
    {
        
    }
    float HP = 3;
    public void Hit()
    {
        if (target_type == Target_Types.Tank)
        {
            HP--;
            if (HP <= 0)
            {
                Target_Shot();
            }
        }
        else if (target_type == Target_Types.Ex)
        {
            //StartCoroutine(Explode());
        }
        else if (target_type == Target_Types.FB)
        {
            StartCoroutine(FB());
        }
        else
        {
            Target_Shot();
        }
    }

    IEnumerator FB()
    {
        player.Destoryed_Target();
        FB_Effect.transform.SetParent(null);
        FB_vfx.gameObject.transform.SetParent(null);
        yield return new WaitForSeconds(Fuse);
        FB_Effect.SetActive(true);
        FB_vfx.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    //IEnumerator Explode()
    //{
    //    player.Destoryed_Target();
    //    yield return new WaitForSeconds(Fuse);
    //    //Ex_vfx.gameObject.SetActive(true);
    //    Instantiate(Ex_vfx.gameObject, transform.position, Quaternion.identity);
    //    Ex_vfx.Play();
    //    player.flash_bang.SetActive(true);
    //    FB = true;
    //    //if (is_visible)
    //    //{
    //    //    player.flash_bang.SetActive(true);
    //    //}
    //    gameObject.transform.localScale = Vector3.zero;
    //    yield return new WaitForSeconds(Flash_Bang_Timer);
    //    player.flash_bang.SetActive(false);
    //    FB = false;
    //}

    void Target_Shot()
    {
        gameObject.SetActive(false);
        player.Destoryed_Target();
    }
}
