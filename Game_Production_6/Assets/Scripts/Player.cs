using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Settings;

public class Player : MonoBehaviour
{
    [SerializeField] Camera Gameplay_Cam;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Win_Screen;
    [SerializeField] GameObject Gameplay_UI;
    [SerializeField] LayerMask Checkpoint_Layer;
    [SerializeField] Player_Movement Player_Movement;
    [SerializeField] ParticleSystem Collect_Effect;
    [SerializeField] ParticleSystem Shoot_Impact_Effect;
    [SerializeField] LineRenderer shoot_effect;
    [SerializeField] private float fade_duration;

    //[SerializeField] Camera Win_Cam;
    //[SerializeField] ParticleSystem Collected_Particle;
    //[SerializeField] ParticleSystem To_Power_Door;
    //[SerializeField] InputActionAsset input_actions;

    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Fire_Point;

    Game_Controller game_controller;
    Audio_Manager audio_manager;
    Extra_Objectives extra_objective;
    Settings settings;
    Timer Timer_cs;
    //public GameObject Door;

    public GameObject Lose_Screen;
    public GameObject flash_bang;
     float Targets_Remaining = 0;
    public float Score = 0;
    Vector3 respawn_pos;
    string current_lvl;

    //int layerMask = ~LayerMask.GetMask("TargetLayer");    
    //[SerializeField] float Timer_cs = 30;
    //public float target_hitted = 0;
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
        game_controller = FindAnyObjectByType<Game_Controller>();
        audio_manager = FindAnyObjectByType<Audio_Manager>();
        extra_objective = FindAnyObjectByType<Extra_Objectives>();
        settings = FindAnyObjectByType<Settings>();
        Timer_cs = FindAnyObjectByType<Timer>();

        Targets_Remaining = GameObject.Find("Targets").transform.childCount;
        Targets_Text.text = "Targets Remaining: " + (Targets_Remaining);
        Time.timeScale = 1;
        audio_manager.Play_Music(audio_manager.Gameplay);
        game_controller.lock_mouse = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        FindAnyObjectByType<Change_Scene>().Setting_Buttons_In_Game();
        extra_objective.all_text.transform.localScale = Vector3.zero;
        respawn_pos = transform.position;

        extra_objective.collectables = 0;
        extra_objective.pots = 0;

        settings.Setting_EO(SceneManager.GetActiveScene().name);

        //extra_objective.current_lvl = SceneManager.GetActiveScene().name;
        //current_lvl = SceneManager.GetActiveScene().name;
        //if (extra_objective.tar_air == false)
        //{

        //}

        //if (current_lvl == "Lvl_1")
        //{
        //    Timer_cs.timer = 30;
        //}
        //else if (current_lvl == "Lvl_2")
        //{
        //    Timer_cs.timer = 45;
        //}
        //else if (current_lvl == "Lvl_3")
        //{
        //    Timer_cs.timer = 15;
        //}
        //else if (current_lvl == "Lvl_4")
        //{
        //    Timer_cs.timer = 30;
        //}

        //if (game_controller.Best_time != 0)
        //    Best_time_Text.text = "Best Time: " + game_controller.Best_time.ToString("F2");
        //else
        //    Best_time_Text.text = "Best Time: None";

        //move_input = input_actions.FindAction("Move");
        //new_room_trigger_pos = transform.position; 
    }

    void Update()
    {
        Other_Actions();
        //Timer_cs -= Time.deltaTime;
        //Timer_Text.text = Timer_cs.ToString("F2");
        //if (Timer_cs <= 0)
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
        else if (Input.GetKeyDown(KeyCode.Alpha0)) ////////////////////////////////////////////////////////////////////////////////////
        {
            Destoryed_Target();
        }
    }

    private IEnumerator Shoot_Effect_Fade_Out(LineRenderer SE)
    {
        Material SE_mat = SE.material;
        for (float i = fade_duration; i >= 0; i -= Time.deltaTime)
        {
            SE_mat.color = new Color(SE_mat.color.r, SE_mat.color.g, SE_mat.color.b, Mathf.Clamp01(i / fade_duration));
            yield return null;
        }
        SE_mat.color = new Color(SE_mat.color.r, SE_mat.color.g, SE_mat.color.b, 0f);
        Destroy(SE);
    }

    void Shooting()
    {
        if (Physics.Raycast(Fire_Point.position, Camera.main.transform.forward, out RaycastHit hit, 100, ~Checkpoint_Layer))
        {
            //Debug.DrawRay(Fire_Point.position, Camera.main.transform.forward * hit.distance, Color.green, 1);
            //shoot_effect.SetPosition(0, Fire_Point.position);
            //shoot_effect.SetPosition(1, hit.point);
            LineRenderer shot_effect_inst = Instantiate(shoot_effect);
            StartCoroutine(Shoot_Effect_Fade_Out(shot_effect_inst));
            shot_effect_inst.SetPosition(0, Fire_Point.position);
            shot_effect_inst.SetPosition(1, hit.point);
            Instantiate(Shoot_Impact_Effect.gameObject, hit.point, Quaternion.identity);

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
                //    extra_objective.two_tar = true;
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
                //        extra_objective.two_tar = true;
                //    }
                //}
            }
            else if (hit_GO.CompareTag("Pot"))
            {
                hit_GO.SetActive(false);
                Update_Score(1);
                extra_objective.pots++;
                extra_objective.Check_EO(Extra_Objectives.EO_Types.Other); //
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
        audio_manager.Play_SFX_One_Shot(audio_manager.Shooting);

        //else
        //{
        //    Debug.DrawRay(Fire_Point.position, Camera.main.transform.forward * 100, Color.red, 1);
        //    //LineRenderer shot_effect_inst = Instantiate(shoot_effect);
        //    shoot_effect.SetPosition(0, Fire_Point.position);
        //    shoot_effect.SetPosition(1, Camera.main.transform.forward * 100);
        //    //Destroy(shot_effect_inst, 1);
        //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            Update_Score(1);
            extra_objective.collectables++;
            extra_objective.Check_EO(Extra_Objectives.EO_Types.Other); //
            Instantiate(Collect_Effect, other.GetComponent<SphereCollider>().transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Checkpoint"))
            respawn_pos = other.transform.position;
        else if (other.CompareTag("Main Menu"))
            FindAnyObjectByType<Change_Scene>().Scene_To_Load("Main_Menu");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Respawn_Y")
            transform.position = respawn_pos;
    }

    public void Destoryed_Target()
    {
        Targets_Remaining--;
        Targets_Text.text = "Targets Remaining: " + (Targets_Remaining);
        extra_objective.Check_EO(Extra_Objectives.EO_Types.Target);

        if (Targets_Remaining <= 0) // win
        {
            Win_Screen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            game_controller.can_open_setting = false;
            extra_objective.Check_EO(Extra_Objectives.EO_Types.Timer);

            //current_lvl = SceneManager.GetActiveScene().name;
            //extra_objective.Set_Completed_EO(current_lvl);
            current_lvl = SceneManager.GetActiveScene().name;

            if (current_lvl == "Lvl_1")
            {
                game_controller.L2_Locked = false;
                if (Score > game_controller.L1_HS)
                    game_controller.L1_HS = Score;
            }
            if (current_lvl == "Lvl_2")
            {
                game_controller.L3_Locked = false;
                if (Score > game_controller.L2_HS)
                    game_controller.L2_HS = Score;
            }
            if (current_lvl == "Lvl_3")
            {
                game_controller.L4_Locked = false;
                if (Score > game_controller.L3_HS)
                    game_controller.L3_HS = Score;
            }
            if (current_lvl == "Lvl_4")
            {
                game_controller.L5_Locked = false;
                if (Score > game_controller.L4_HS)
                    game_controller.L4_HS = Score;
            }
            if (current_lvl == "Lvl_5")
            {
                game_controller.L6_Locked = false;
                if (Score > game_controller.L5_HS)
                    game_controller.L5_HS = Score;
            }
            Time.timeScale = 0;

            //extra_objective.beat_lvl = true;
            //target_hitted = 1;
        }
    }

    void Hit_target(GameObject Hit_GO)
    {
        if (Hit_GO.layer == 6)
            Update_Score(1);
        if (Hit_GO.layer == 7)
            Update_Score(2);
        if (Hit_GO.layer == 8)
            Update_Score(3);
        Hit_GO.GetComponentInParent<Target>().Hit();
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Collectable"))
    //    {
    //        Collect();
    //        other.gameObject.SetActive(false);
    //        audio_manager.Play_SFX_One_Shot(audio_manager.Shooting);
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
    //        audio_manager.Play_Music(audio_manager.Win_OST);
    //        game_controller.lock_mouse = false;
    //        if (timer < game_controller.Best_time || game_controller.Best_time == 0)
    //        {
    //            game_controller.Best_time = timer;
    //            Best_time_end_Text.text = "Best Time: " + game_controller.Best_time.ToString("F2");
    //            game_controller.Best_time_Text.text = "Best Time: " + game_controller.Best_time.ToString("F2");
    //            PlayerPrefs.SetFloat("Best_Time", game_controller.Best_time);
    //        }
    //        else
    //            Best_time_end_Text.text = "Best Time: " + game_controller.Best_time.ToString("F2");
    //    }
    //}
}
