using System.Collections;
using UnityEngine;

public class Exploding_Projectile : MonoBehaviour
{
    [SerializeField] Type type;
    [SerializeField] GameObject EX;
    [SerializeField] float proj_move_speed;
    [SerializeField] float Disable_Hitbox_Timer; // 0.5
    GameObject Player;
    Vector3 ex_pos;
    enum Type
    {
        Proj,
        Ex
    }
    void Start()
    {
        Player = FindAnyObjectByType<Player>().gameObject;
        ex_pos = Player.transform.position;
    }

    void Update()
    {
        if (type == Type.Proj)
        {
            transform.position = Vector3.MoveTowards(transform.position, ex_pos, Time.deltaTime * proj_move_speed);
            if (transform.position == ex_pos)
            {
                Explode();
            }
        }
        else if (type == Type.Ex)
        {
            StartCoroutine(Disable_Hitbox());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == Type.Proj)
            {
                Explode();
            }
            else if (type == Type.Ex)
            {
                StartCoroutine(Player.GetComponent<Player_Movement>().Slow_Player());
            }
        }
        else if (other.CompareTag("Lvl"))
        {
            if (type == Type.Proj)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Instantiate(EX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Disable_Hitbox()
    {
        yield return new WaitForSeconds(Disable_Hitbox_Timer);
        GetComponent<SphereCollider>().enabled = false;
    }
}
