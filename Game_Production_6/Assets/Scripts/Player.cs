using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Camera Gameplay_Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Win_Screen;
    [SerializeField] GameObject Gameplay_UI;
    [SerializeField] Player_Movement Player_Movement;
    [SerializeField] ParticleSystem Player_Death;
    [SerializeField] LineRenderer shoot_effect;
    [SerializeField] private float fade_duration;

    //[SerializeField] Camera Win_Cam;
    //[SerializeField] ParticleSystem Collected_Particle;
    //[SerializeField] ParticleSystem To_Power_Door;
    //[SerializeField] InputActionAsset input_actions;

    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Fire_Point;

    Game_Controller Game_Controller;
    Audio_Manager Audio_Manager;
    Extra_Objectives Extra_Objectives;
    Timer Timer;
    //public GameObject Door;

    public GameObject Lose_Screen;
    public GameObject flash_bang;
     float Targets_Remaining = 0;
    public float Score = 0;
    public float target_hitted = 0;
    Vector3 respawn_pos;

    //[SerializeField] float Timer = 30;
    //public float door_power = 2;
    //Vector3 new_room_trigger_pos;

    [Header("Text")]
    [SerializeField] TMP_Text Targets_Text;
    [SerializeField] TMP_Text Final_Time_Text;
    [SerializeField] TMP_Text Best_time_Text;
    [SerializeField] TMP_Text Best_time_end_Text;
    [SerializeField] TMP_Text Score_Text;
    //[SerializeField] TMP_Text Timer_Text;

    void Start()
    {
        //new_room_trigger_pos = transform.position;
        Game_Controller = FindAnyObjectByType<Game_Controller>();
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        Extra_Objectives = FindAnyObjectByType<Extra_Objectives>();
        current_lvl = SceneManager.GetActiveScene().name;

        //GameObject targets = GameObject.Find("Targets");
        Targets_Remaining = GameObject.Find("Targets").transform.childCount;
        Targets_Text.text = "Targets Remaining: " + (Targets_Remaining);
        Time.timeScale = 1;
        Audio_Manager.Play_Music(Audio_Manager.Gameplay);
        Game_Controller.lock_mouse = true;
        FindAnyObjectByType<Change_Scene>().Setting_Buttons_In_Game();
        Extra_Objectives.Get_current_lvl(SceneManager.GetActiveScene().name);
        Extra_Objectives.all_text.transform.localScale = Vector3.zero;
        respawn_pos = transform.position;
        Timer = FindAnyObjectByType<Timer>();
        if (current_lvl == "Lvl_1")
        {
            Timer.timer = 30;
        }
        else if (current_lvl == "Lvl_2")
        {
            Timer.timer = 45;
        }
        else if (current_lvl == "Lvl_3")
        {
            Timer.timer = 15;
        }
        else if (current_lvl == "Lvl_4")
        {
            Timer.timer = 30;
        }

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            Shooting();
            //GameObject player_bullet = Instantiate(Bullet, Camera.main.transform.position, Camera.main.transform.localRotation);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Destoryed_Target();
        }
            //else if (Input.GetKeyDown(KeyCode.R))
            //    Door.GetComponent<Door>().enabled = true;
    }

    private IEnumerator Shoot_Effect_Fade_Out()
    {
        Material SE_mat = shoot_effect.material;
        for (float i = fade_duration; i >= 0; i -= Time.deltaTime)
        {
            SE_mat.color = new Color(SE_mat.color.r, SE_mat.color.g, SE_mat.color.b, Mathf.Clamp01(i / fade_duration));
            yield return null;
        }
        SE_mat.color = new Color(SE_mat.color.r, SE_mat.color.g, SE_mat.color.b, 0f);
    }

    void Shooting()
    {

        if (Physics.Raycast(Fire_Point.position, Camera.main.transform.forward, out RaycastHit hit, 100))
        {
            StartCoroutine(Shoot_Effect_Fade_Out());
            Debug.DrawRay(Fire_Point.position, Camera.main.transform.forward * hit.distance, Color.green, 1);
            //LineRenderer shot_effect_inst = Instantiate(shoot_effect);
            shoot_effect.SetPosition(0, Fire_Point.position);
            shoot_effect.SetPosition(1, hit.point);
            //Destroy(shot_effect_inst, 1);
            GameObject hit_GO = hit.collider.gameObject;
            if (hit_GO.CompareTag("Target") || hit_GO.CompareTag("Target_Tank"))
            {
                Hit_target(hit_GO);
                //if (hit_GO.CompareTag("Target"))
                //{
                //    Shooting();
                //}
                //target_hitted++;
                //if (target_hitted >= 2)
                //{
                //    Extra_Objectives.two_tar = true;
                //}
                //if (Physics.Raycast(Fire_Point.position, Camera.main.transform.forward, out hit, 100) && PU_pierce > 0)
                //{
                //    shoot_effect.SetPosition(0, Fire_Point.position);
                //    shoot_effect.SetPosition(1, hit.point);
                //    hit_GO = hit.collider.gameObject;
                //    if (hit_GO.CompareTag("Target"))
                //    {
                //        Hit_target(hit_GO);
                //        Update_Score(2);
                //        Extra_Objectives.two_tar = true;
                //    }
                //}
            }
            else if (hit_GO.CompareTag("Pot"))
            {
                hit_GO.SetActive(false);
                Update_Score(1);
                if (Extra_Objectives.pots > 0)
                    Extra_Objectives.pots--;
                else
                    Extra_Objectives.Check_EO();
            }
            //else if (hit_GO.CompareTag("Pierce"))
            //{
            //    PU_pierce = 4;
            //    hit_GO.SetActive(false);
            //}
            //PU_pierce--;
            //bool a = true;
            //if (hit_GO.CompareTag("Target_Tank"))
            //{
            //    a = false;
            //}
            //if (hit_GO.layer != 3 && hit_GO.tag != "Target_Tank")
            //{
            //    Shooting();
            //}
            //else
            //    target_hitted = 0;
        }
        Audio_Manager.Play_SFX_One_Shot(Audio_Manager.Shooting);

        //else
        //{
        //    Debug.DrawRay(Fire_Point.position, Camera.main.transform.forward * 100, Color.red, 1);
        //    //LineRenderer shot_effect_inst = Instantiate(shoot_effect);
        //    shoot_effect.SetPosition(0, Fire_Point.position);
        //    shoot_effect.SetPosition(1, Camera.main.transform.forward * 100);
        //    //Destroy(shot_effect_inst, 1);
        //}

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
            Update_Score(1);
            if (Extra_Objectives.collectables > 0)
                Extra_Objectives.collectables--;
            else
                Extra_Objectives.Check_EO();

        }
        else if (other.gameObject.name == "Respawn_Y")
        {
            transform.position = respawn_pos;
        }
        else if (other.CompareTag("Main Menu"))
        {
            FindAnyObjectByType<Change_Scene>().Scene_To_Load("Main_Menu");
        }

    }
    string current_lvl;

    public void Destoryed_Target()
    {
        Targets_Remaining--;
        Targets_Text.text = "Targets Remaining: " + (Targets_Remaining);
        target_hitted = 1;
        Extra_Objectives.Check_EO();

        if (Targets_Remaining <= 0)
        {
            Win_Screen.SetActive(true);
            current_lvl = SceneManager.GetActiveScene().name;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Game_Controller.can_open_setting = false;
            //Extra_Objectives.Set_Completed_EO(current_lvl);
            if (current_lvl == "Lvl_1")
                Game_Controller.L2_Locked = false;
            if (current_lvl == "Lvl_2")
                Game_Controller.L3_Locked = false;
            if (current_lvl == "Lvl_3")
                Game_Controller.L4_Locked = false;
        }
    }

    void Hit_target(GameObject Hit_GO)
    {
        Hit_GO.GetComponentInParent<Target>().Hit();
        if (Hit_GO.layer == 6)
            Update_Score(1);
        if (Hit_GO.layer == 7)
            Update_Score(2);
        if (Hit_GO.layer == 8)
            Update_Score(3);
    }

    void Update_Score(float val)
    {
        Score += val;
        Score_Text.text = "Score: " + Score;
    }

    public void In_How_Play_Lvl()
    {
        Targets_Remaining = int.MaxValue;
    }
}
