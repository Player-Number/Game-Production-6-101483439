using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Camera Gameplay_Cam;
    //[SerializeField] Camera Win_Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Win_Screen;
    [SerializeField] GameObject Gameplay_UI;
    [SerializeField] Player_Movement Player_Movement;
    //[SerializeField] ParticleSystem Collected_Particle;
    //[SerializeField] ParticleSystem To_Power_Door;
    [SerializeField] ParticleSystem Player_Death;
    [SerializeField] LineRenderer shoot_effect;
    //[SerializeField] InputActionAsset input_actions;
    
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Fire_Point;
    public GameObject Lose_Screen;

    Game_Controller Game_Controller;
    Audio_Manager Audio_Manager;
    Extra_Objectives Extra_Objectives;
    //Rigidbody rb;
    //public GameObject Door;

    public float Targets_Remaining = 3;
    //[SerializeField] float Timer = 30;
    //public float door_power = 2;
    //Vector3 new_room_trigger_pos;

    [Header("Text")]
    [SerializeField] TMP_Text Targets_Text;
    //[SerializeField] TMP_Text Timer_Text;
    [SerializeField] TMP_Text Final_Time_Text;
    [SerializeField] TMP_Text Best_time_Text;
    [SerializeField] TMP_Text Best_time_end_Text;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //new_room_trigger_pos = transform.position;
        GameObject targets = GameObject.Find("Targets");
        Targets_Remaining = targets.transform.childCount;
        Targets_Text.text = "Targets Remaining: " + (Targets_Remaining);
        Time.timeScale = 1;
        Game_Controller = FindAnyObjectByType<Game_Controller>();
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        Extra_Objectives = FindAnyObjectByType<Extra_Objectives>();
        Audio_Manager.Play_Music(Audio_Manager.Gameplay);
        Game_Controller.lock_mouse = true;
        FindAnyObjectByType<Change_Scene>().Setting_Buttons_In_Game();
        Extra_Objectives.Get_current_lvl(SceneManager.GetActiveScene().name);
        //if (Game_Controller.Best_time != 0)
        //    Best_time_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
        //else
        //    Best_time_Text.text = "Best Time: None";

        //move_input = input_actions.FindAction("Move");
    }

    void Update()
    {
        Other_Actions();
        //Timer -= Time.deltaTime;
        //Timer_Text.text = Timer.ToString("F2");
        //if (Timer <= 0)
        //{
        //    Lose_Screen.SetActive(true);
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    Time.timeScale = 0;
        //    Timer_Text.text = "00.00";
        //}
    }

    private void Other_Actions()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
            //GameObject player_bullet = Instantiate(Bullet, Camera.main.transform.position, Camera.main.transform.localRotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Shot_Target();
        }
            //else if (Input.GetKeyDown(KeyCode.R))
            //    Door.GetComponent<Door>().enabled = true;
    }

    void Shooting()
    {
        RaycastHit hit;
        Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Shooting);
        if (Physics.Raycast(Fire_Point.position, Camera.main.transform.forward, out hit, 100))
        {
            Debug.DrawRay(Fire_Point.position, Camera.main.transform.forward * hit.distance, Color.green, 1);
            //LineRenderer shot_effect_inst = Instantiate(shoot_effect);
            shoot_effect.SetPosition(0, Fire_Point.position);
            shoot_effect.SetPosition(1, hit.point);
            //Destroy(shot_effect_inst, 1);
            GameObject hit_GO = hit.collider.gameObject;
            if (hit_GO.CompareTag("Target"))
            {
                hit_GO.SetActive(false);
                Shot_Target();
            }
            else if (hit_GO.CompareTag("Pot"))
            {
                hit_GO.SetActive(false);
                if (Extra_Objectives.pots > 0)
                    Extra_Objectives.pots--;

            }
        }
        else
        {
            Debug.DrawRay(Fire_Point.position, Camera.main.transform.forward * 100, Color.red, 1);
            //LineRenderer shot_effect_inst = Instantiate(shoot_effect);
            shoot_effect.SetPosition(0, Fire_Point.position);
            shoot_effect.SetPosition(1, Camera.main.transform.forward * 100);
            //Destroy(shot_effect_inst, 1);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Collectable"))
    //    {
    //        Collect();
    //        other.gameObject.SetActive(false);
    //        Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Shooting);
    //        ParticleSystem Collected_Particle_inst = Instantiate(Collected_Particle, other.transform.position, Quaternion.identity);
    //        if (!Collected_Particle_inst.isEmitting)
    //        {
    //            Destroy(Collected_Particle_inst);
    //        }
    //        ParticleSystem To_Power_Door_inst = Instantiate(To_Power_Door, other.transform.position, Quaternion.identity);
    //        //To_Power_Door_inst.gameObject.GetComponent<To_Power_Door>().Door = Door;
    //    }
    //    else if (other.CompareTag("New_Room"))
    //    {
    //        new_room_trigger_pos = other.transform.position;
    //        Targets_Text.text = "Collectable Remaining: " + (Targets_Remaining);
    //    }
    //    else if (other.CompareTag("Death"))
    //    {
    //        Player_Death.transform.position = transform.position;
    //        Player_Death.Play();
    //        transform.position = new_room_trigger_pos;
    //        rb.linearVelocity = Vector3.zero;
    //    }
    //    else if (other.gameObject.name == "Win_Trigger")
    //    {
    //        Win_Screen.SetActive(true);
    //        Gameplay_UI.SetActive(false);
    //        Gameplay_Cam.gameObject.SetActive(false);
    //        Win_Cam.gameObject.SetActive(true);
    //        Final_Time_Text.text = "Final Time: " + timer.ToString("F2");
    //        Cursor.visible = true;
    //        Cursor.lockState = CursorLockMode.None;
    //        Audio_Manager.Play_Music(Audio_Manager.Win_OST);
    //        Game_Controller.lock_mouse = false;
    //        if (timer < Game_Controller.Best_time || Game_Controller.Best_time == 0)
    //        {
    //            Game_Controller.Best_time = timer;
    //            Best_time_end_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
    //            Game_Controller.Best_time_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
    //            PlayerPrefs.SetFloat("Best_Time", Game_Controller.Best_time);
    //        }
    //        else
    //            Best_time_end_Text.text = "Best Time: " + Game_Controller.Best_time.ToString("F2");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            if (Extra_Objectives.collectables > 0)
                Extra_Objectives.collectables--;
        }
    }
    string currnt_lvl;
    public void Shot_Target()
    {
        Targets_Remaining--;
        Targets_Text.text = "Targets Remaining: " + (Targets_Remaining);
        if (Targets_Remaining <= 0)
        {
            Win_Screen.SetActive(true);
            currnt_lvl = SceneManager.GetActiveScene().name;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Game_Controller.can_open_setting = false;

            if (FindAnyObjectByType<Timer>().timer >= 15)
                Extra_Objectives.O1 = Color.green;
            if (Extra_Objectives.collectables <= 0)
                Extra_Objectives.O2 = Color.green;
            if (Extra_Objectives.pots <= 0)
                Extra_Objectives.O3 = Color.green;

            Extra_Objectives.Check_EO(currnt_lvl);
            if (currnt_lvl == "Lvl_1")
                Game_Controller.L2_Locked = false;
            if (currnt_lvl == "Lvl_2")
                Game_Controller.L3_Locked = false;
            if (currnt_lvl == "Lvl_3")
                Game_Controller.L3_Locked = false;

        }
    }
}
