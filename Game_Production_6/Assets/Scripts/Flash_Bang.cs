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
            FindAnyObjectByType<Timer>().FB_in_effect_timer = 2;
            gameObject.SetActive(false);
        }
        else
            gameObject.SetActive(false);
    }
}
