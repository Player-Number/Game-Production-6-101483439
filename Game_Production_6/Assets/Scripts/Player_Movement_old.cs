using UnityEngine;

public class Player_Movement_old : MonoBehaviour
{
    Rigidbody rb;

    public Transform Orientation;

    [Header("Move")]
    Vector3 move_dir;
    float horizontal_input;
    float vertical_input;

    [Header("Jump")]
    public float player_height;
    public float grounded_drag;
    public LayerMask ground_layer;
    public bool is_grounded;

    public float jump_force;
    public float jump_cool;
    bool ready_to_jump = true;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Player_Input();
    }

    private void Move()
    {
        move_dir = Orientation.forward * vertical_input + Orientation.right * horizontal_input;

        //if (state == Movement_State.Running)
        //    rb.AddForce(10 * move_speed * move_dir.normalized, ForceMode.Force);
        //else if (!is_grounded)
        //    rb.AddForce(10 * air_speed * move_speed * move_dir.normalized, ForceMode.Force);
    }

    void Player_Input()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && is_grounded && ready_to_jump)
        {
            Jump(jump_force);
            ready_to_jump = false;
            //Jump_VFX.transform.position = new(transform.position.x, transform.position.y - 1, transform.position.z);
            //Jump_VFX.Play();
            //Invoke(nameof(Reset_Jump), jump_cool);
        }
    }

    public void Jump(float jump_force)
    {
        rb.linearVelocity = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
        ready_to_jump = false;
        Invoke(nameof(Reset_Jump), jump_cool);
    }

    void Reset_Jump()
    {
        ready_to_jump = true;
    }

}
