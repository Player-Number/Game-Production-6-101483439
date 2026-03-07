using UnityEngine;

public class How_to_Play_Target : MonoBehaviour
{
    [SerializeField] GameObject Tar;
    float timer = 1;
    //Vector3 respawn_pos;
    void Start()
    {
        FindAnyObjectByType<Timer>().How_Play_Lvl_Timer();
        FindAnyObjectByType<Player>().In_How_Play_Lvl();

        //respawn_pos = Tar.transform.position;
    }

    void Update()
    {
        if (!Tar.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Tar.SetActive(true);
                timer = 1;
                //Tar.transform.position = respawn_pos;
            }
            //Debug.Log(!Tar.activeInHierarchy);
        }
    }
}
