using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    void Start()
    {
        Destroy(gameObject, 3);
    }

    void Update()
    {
        transform.position += transform.forward * speed;
    }
}
