using System.Collections;
using UnityEngine;

public class Flash_Bang : MonoBehaviour
{
    [SerializeField] float Flash_Bang_Timer; // 2

    void Start()
    {

    }

    void Update()
    {
        if (GetComponent<MeshRenderer>().isVisible)
        {
            if (FindAnyObjectByType<Timer>() != null)
            {
                FindAnyObjectByType<Timer>().FB_in_effect_timer = 2;
            }
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
