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
    [SerializeField] GameObject Outer; 
    [SerializeField] GameObject center; 
    [SerializeField] GameObject projectile; 
    [SerializeField] List<GameObject> Obj_to_destory; 
    [SerializeField] Material Fuse_mat; 
    [SerializeField] float Fuse; // 2
    [SerializeField] float Fuse_ticking_timer; // 0.5

    Player player;
    Material og_Fuse_mat; 
    public enum Target_Types
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
        if (target_type == Target_Types.FB)
        {
            og_Fuse_mat = center.GetComponent<MeshRenderer>().material;
        }
        else if (target_type == Target_Types.Ex)
        {
            og_Fuse_mat = center.GetComponent<MeshRenderer>().material;

            //Ex_vfx = GameObject.Find("Ex_Vfx").GetComponent<ParticleSystem>();
            //Ex_vfx.Stop();
        }
    }

    //void Update()
    //{

    //}
    float HP = 3;
    public void Hit()
    {
        if (target_type == Target_Types.Tank)
        {
            HP--;
            if (HP <= 0)
                Target_Shot();
        }
        else if (target_type == Target_Types.Ex)
        {
            StartCoroutine(Explode());
            StartCoroutine(Fuse_ticking(Fuse_mat));
        }
        else if (target_type == Target_Types.FB)
        {
            StartCoroutine(FB());
            StartCoroutine(Fuse_ticking(Fuse_mat));
        }
        else if (target_type == Target_Types.Shoot)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            Target_Shot();
        }
        else
        {
            Target_Shot();
        }
    }

    IEnumerator FB()
    {
        player.Destoryed_Target();
        //FB_Effect.transform.SetParent(null);
        //FB_vfx.gameObject.transform.SetParent(null);
        yield return new WaitForSeconds(Fuse);
        Instantiate(FB_Effect, transform.position, Quaternion.identity);
        Instantiate(FB_vfx, transform.position, Quaternion.identity);
        //FB_Effect.SetActive(true);
        //FB_vfx.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    IEnumerator Explode()
    {
        player.Destoryed_Target();
        yield return new WaitForSeconds(Fuse);
        ParticleSystem Ex_vfx_inst = Instantiate(Ex_vfx, transform.position, Quaternion.identity);
        Ex_vfx_inst.Play();
        foreach (GameObject go in Obj_to_destory)
        {
            go.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    IEnumerator Fuse_ticking(Material fuse_mat_ticking)
    {
        //player.Destoryed_Target();

        BoxCollider[] all_colliders = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider hitboxs in all_colliders)
            hitboxs.enabled = false;
        for (int i = 0; i < 10; i++)
        {
            Outer.GetComponent<MeshRenderer>().material = fuse_mat_ticking;
            center.GetComponent<MeshRenderer>().material = fuse_mat_ticking;
            if (fuse_mat_ticking == Fuse_mat)
                fuse_mat_ticking = og_Fuse_mat;
            else if (fuse_mat_ticking == og_Fuse_mat)
                fuse_mat_ticking = Fuse_mat;
            yield return new WaitForSeconds(Fuse_ticking_timer);
        }

        //if (target_type == Target_Types.Ex)
        //{
        //    Explode();
        //}
        //else if (target_type == Target_Types.FB)
        //{
        //    FB();
        //}

    }

    void Target_Shot()
    {
        player.Destoryed_Target();
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

    //void FB()
    //{
    //    Instantiate(FB_Effect, transform.position, Quaternion.identity);
    //    Instantiate(FB_vfx, transform.position, Quaternion.identity);
    //    gameObject.SetActive(false);
    //}
    //void Explode()
    //{
    //    ParticleSystem Ex_vfx_inst = Instantiate(Ex_vfx, transform.position, Quaternion.identity);
    //    Ex_vfx_inst.Play();
    //    foreach (GameObject go in Obj_to_destory)
    //        go.SetActive(false);
    //    gameObject.SetActive(false);
    //}

}
