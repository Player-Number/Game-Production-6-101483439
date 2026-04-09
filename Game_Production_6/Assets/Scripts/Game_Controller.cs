using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] Audio_Manager Audio_Manager;
    [SerializeField] GameObject Setting_BG_Not_Visible;
    Change_Scene Change_Scene;

    public GameObject Setting_Menu;
    public GameObject Close_button;
    public GameObject Resume_button;
    public GameObject To_Main_Menu_button;
    public Canvas Main_Menu;
    public bool lock_mouse = false;
    public bool can_open_setting = true;
    bool is_setting_active = false;
    //public TMP_Text Best_time_Text;
    //public float Best_time = 0; // int.MaxValue

    public bool L2_Locked = true;
    public bool L3_Locked = true;
    public bool L4_Locked = true;
    public bool L5_Locked = true;
    public bool L6_Locked = true;

    public float L1_HS = 0;
    public float L2_HS = 0;
    public float L3_HS = 0;
    public float L4_HS = 0;
    public float L5_HS = 0;
    public float L6_HS = 0;
    //public Slider FOV_Slider;
    //public bool disable_pause = true;
    //[SerializeField] TMP_Text Sensitivity_num;
    //public Slider Sensitivity_Slider;
    //public GameObject Setting_button;

    public static Game_Controller Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    //private void Start()
    //{
    //    //Load_Best_Time();
    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && can_open_setting) // && disable_pause == false (pause
        {
            if (!is_setting_active)
            {
                is_setting_active = true;
                Setting_Menu.gameObject.SetActive(is_setting_active);
                Audio_Manager.Play_SFX_Button_Pressed();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Audio_Manager.Pause_SFX();
                if (SceneManager.GetActiveScene().name != "Game_Scene")
                {
                    Setting_BG_Not_Visible.SetActive(is_setting_active);
                }
            }
            else
            {
                Resume();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Time.timeScale = 1;
        //}
    }

    public void Resume()
    {
        is_setting_active = false;
        Time.timeScale = 1.0f;
        Setting_Menu.SetActive(is_setting_active);
        Audio_Manager.Play_SFX_Button_Pressed();
        Audio_Manager.Play_SFX(); // resume gameplay audio
        Setting_BG_Not_Visible.SetActive(is_setting_active);

        if (lock_mouse) // stop lock_mouse when return to main menu
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void To_Main_Menu()
    {
        Change_Scene = FindAnyObjectByType<Change_Scene>();
        Change_Scene.Scene_To_Load("Main_Menu");
        Resume();
        //game_controller.GetComponent<game_controller>().Best_time_Text.gameObject.SetActive(true);
        //game_controller.GetComponent<game_controller>().Best_time_Text.text = "Best Time: " + best_time.ToString("F2");
    }

    //public void Check_Scene(string name)
    //{
    //    //GetComponent<extra_objective>().all_text.transform.localScale = Vector3.zero;
    //    //if (name == "Lvl_Select")
    //    //{
    //    //    GetComponent<extra_objective>().all_text.transform.localScale = Vector3.one;

    //    //    //for (int i = 0; i < extra_objective.all_text.transform.childCount; i++)
    //    //    //{
    //    //    //    Debug.Log(i);
    //    //    //    extra_objective.all_text.transform.GetChild(i).gameObject.SetActive(true);
    //    //    //}
    //    //    //extra_objective.all_text.SetActive(true);
    //    //    //if (extra_objective.L1_T == true)
    //    //    //    GameObject.Find("L1 15 sec").GetComponent<TMP_Text>().color = Color.gray;
    //    //    //if (extra_objective.L1_C == true)
    //    //    //    GameObject.Find("L1 2 spheres").GetComponent<TMP_Text>().color = Color.gray;
    //    //    //if (extra_objective.L1_P == true)
    //    //    //{
    //    //    //    GameObject.Find("L1 3 pots").GetComponent<TMP_Text>().color = Color.gray;
    //    //    //    Debug.Log("extra_objective.L1_P == true");
    //    //    //}

    //    //}
    //}

    //public void Load_Best_Time()
    //{
    //    if (PlayerPrefs.HasKey("Best_Time"))
    //    {
    //        Best_time = PlayerPrefs.GetFloat("Best_Time");
    //        if (Best_time != 0)
    //        {
    //            Best_time_Text.text = "Best Time: " + Best_time.ToString("F2");
    //        }
    //    }
    //}

    //public void Close_Settings()
    //{
    //    Setting_Menu.SetActive(false);
    //    audio_manager.Play_SFX_Button_Pressed();
    //    is_setting_active = false;
    //    //Setting_button.SetActive(true);
    //}

    //public void On_Val_Changed(TMP_Text Val_Text, Slider Slider)
    //{
    //    Val_Text.text = Slider.value.ToString();
    //}

    //public void Update_Setting_Num(TMP_Text Val_Text)
    //{
    //    Val_Text.text = GetComponent<Slider>().value.ToString("F0");
    //}


    //public void To_Game()
    //{
    //    button_pressed.Play();
    //    SceneManager.LoadScene("Game_Scene");
    //}

    //public void Quit()
    //{
    //    button_pressed.Play();
    //    Application.Quit();
    //}
}
