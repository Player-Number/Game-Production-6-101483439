using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Target_Types target_type; // 2.5

    [Header("Moving")]
    [SerializeField] float hor_speed; // 2.5
    [SerializeField] float ver_speed; // 2.5
    [SerializeField] float hor_dis; // 5
    [SerializeField] float ver_dis; // 5
    [SerializeField] float custom_move_speed_factor;

    [SerializeField] List<Transform> path_points = new();
    List<Vector3> destinations = new();
    Vector3 start_pos;
    Vector3 end_pos;
    float timer = 0;

    int destination_index = 0;
    float z;
    enum Target_Types
    {
        Still,
        Hor,
        Ver,
        Diagonal_Right,
        Diagonal_Left,
        Custom_move,
        Tank
    }
    void Start()
    {
        foreach (Transform t in path_points)
        {
            destinations.Add(t.position);
        }
        destinations.Add(transform.position);

        start_pos = transform.position;
        end_pos = destinations[destination_index];

        z = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (target_type == Target_Types.Custom_move)
        {
            if (timer < 1)
                timer += custom_move_speed_factor;
            else
            {
                timer = 0;
                destination_index++;
                if (destination_index >= destinations.Count)
                    destination_index = 0;
                start_pos = end_pos;
                end_pos = destinations[destination_index];
            }
        }
    }

    void Update()
    {
        switch (target_type)
        {
            case
                Target_Types.Hor:
                float y = transform.position.y;
                transform.position = new(Mathf.PingPong(hor_speed * Time.time, hor_dis) + start_pos.x, y, z);
                break;
            case Target_Types.Ver:
                float x = transform.position.x;
                transform.position = new(x, Mathf.PingPong(ver_speed * Time.time, ver_dis) + start_pos.y, z);
                break;
            case Target_Types.Diagonal_Right:
                transform.position = new(Mathf.PingPong(hor_speed * Time.time, hor_dis) + start_pos.x,
                    Mathf.PingPong(ver_speed * Time.time, ver_dis) + start_pos.y, z);
                break;
            case Target_Types.Diagonal_Left:
                transform.position = new(start_pos.x - Mathf.PingPong(hor_speed * Time.time, hor_dis),
                    Mathf.PingPong(ver_speed * Time.time, ver_dis) + start_pos.y, z);
                break;
            case Target_Types.Custom_move:
                transform.position = Vector3.Lerp(start_pos, end_pos, timer);
                break;
        }
    }
    float HP = 3;
    public void Hit()
    {
        if (target_type == Target_Types.Tank)
        {
            HP--;
            if (HP <= 0)
            {
                gameObject.SetActive(false);
                FindAnyObjectByType<Player>().Destoryed_Target();
            }
        }
        else
        {
            gameObject.SetActive(false);
            FindAnyObjectByType<Player>().Destoryed_Target();
        }
    }
}
