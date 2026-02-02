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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            FindAnyObjectByType<Player>().Destoryed_Target();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
