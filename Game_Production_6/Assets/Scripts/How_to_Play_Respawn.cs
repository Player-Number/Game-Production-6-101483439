using System.Collections.Generic;
using UnityEngine;

public class How_to_Play_Respawn : MonoBehaviour
{
    [SerializeField] List<GameObject> Tar;
    float timer = 1;
    void Start()
    {
        FindAnyObjectByType<Timer>().How_Play_Lvl_Timer();
    }

    void Update()
    {
        foreach (GameObject obj in Tar)
        {
            if (!obj.activeInHierarchy)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    obj.SetActive(true);
                    timer = 1;
                    FindAnyObjectByType<Player>().In_How_Play_Lvl();
                    FindAnyObjectByType<Timer>().How_Play_Lvl_Timer();
                }
                //Debug.Log(!Tar.activeInHierarchy);
            }
        }
    }
}
