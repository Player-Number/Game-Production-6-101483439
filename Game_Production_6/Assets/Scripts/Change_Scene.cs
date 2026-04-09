using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Change_Scene : MonoBehaviour
{
    Game_Controller game_controller;
    Audio_Manager Audio_Manager;
    //Extra_Objectives Extra_Objectives;
    //string scene_name;
    //float delay_load_timer = 0.25f;
    //bool to_delay_load = false;

    void Start()
    {
        game_controller = FindAnyObjectByType<Game_Controller>();
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
        //Extra_Objectives = FindAnyObjectByType<Extra_Objectives>();
    }

    //void Update()
    //{
    //    //if (to_delay_load)
    //    //{
    //    //    delay_load_timer -= Time.deltaTime;
    //    //    if (delay_load_timer <= 0)
    //    //    {
    //    //        SceneManager.LoadScene(name);
    //    //        if (name != "Main_Menu")
    //    //        {
    //    //            Menu.Best_time_Text.gameObject.SetActive(false);
    //    //        }
    //    //        else
    //    //            Menu.Best_time_Text.gameObject.SetActive(true);
    //    //    }
    //    //}
    //}

    public void Scene_To_Load(string name)
    {
        SceneManager.LoadScene(name);
        //game_controller.Check_Scene(name);
        //extra_objective.all_text.transform.localScale = Vector3.zero;
        if (name == "Controls") // name == "How_Play" || 
        {
            Audio_Manager.Play_Music(Audio_Manager.Other_Menu);
            Setting_Buttons_Not_In_Game();
            //game_controller.Best_time_Text.gameObject.SetActive(false);
        }
        else if (name == "Main_Menu")
        {
            Time.timeScale = 1;
            Setting_Buttons_Not_In_Game();
            game_controller.can_open_setting = true;
            game_controller.lock_mouse = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Audio_Manager.Play_Music(Audio_Manager.Main_Menu);
            //audio_manager.Stop_Music();
            //game_controller.Best_time_Text.gameObject.SetActive(true);
            //GameObject.Find("Panel_BG").GetComponent<Image>().material = null;
        }
        else if (name == "Lvl_Select")
        {
            Setting_Buttons_Not_In_Game();
        }
        else
        {
            Setting_Buttons_In_Game();
        }
        Audio_Manager.Play_SFX_Button_Pressed();
        //else if (name == "Lvl_Select")
        //{
        //    extra_objective.all_text.transform.localScale = Vector3.one;

        //    //for (int i = 0; i < extra_objective.all_text.transform.childCount; i++)
        //    //{
        //    //    Debug.Log(i);
        //    //    extra_objective.all_text.transform.GetChild(i).gameObject.SetActive(true);
        //    //}
        //    //extra_objective.all_text.SetActive(true);
        //    //if (extra_objective.L1_T == true)
        //    //    GameObject.Find("L1 15 sec").GetComponent<TMP_Text>().color = Color.gray;
        //    //if (extra_objective.L1_C == true)
        //    //    GameObject.Find("L1 2 spheres").GetComponent<TMP_Text>().color = Color.gray;
        //    //if (extra_objective.L1_P == true)
        //    //{
        //    //    GameObject.Find("L1 3 pots").GetComponent<TMP_Text>().color = Color.gray;
        //    //    Debug.Log("extra_objective.L1_P == true");
        //    //}

        //}
        //if (name != "Lvl_Select")
        //{
        //    extra_objective.all_text.transform.localScale = Vector3.zero;
        //    //for (int i = 0; i < extra_objective.all_text.transform.childCount; i++)
        //    //{
        //    //    Debug.Log(i);
        //    //    extra_objective.all_text.transform.GetChild(i).gameObject.SetActive(false);
        //    //}

        //    //extra_objective.all_text.SetActive(false);

        //}
        //else if (name == "Game_Scene")
        //{
        //    audio_manager.Play_Music(audio_manager.Gameplay);
        //    //game_controller.Best_time_Text.gameObject.SetActive(false);
        //    game_controller.lock_mouse = true;
        //    //GameObject.Find("Panel_BG").GetComponent<Image>().material = game_controller.Setting_BG_visible;
        //    Setting_Buttons_In_Game();
        //}
        //SceneManager.LoadScene(name);


        //else if (name != "Main_Menu")
        //{
        //    game_controller.Best_time_Text.gameObject.SetActive(false);
        //    game_controller.Close_button.SetActive(false);
        //    game_controller.Resume_button.SetActive(true);
        //    game_controller.To_Main_Menu_button.SetActive(true);
        //    game_controller.disable_pause = false;
        //}
        //else
        //{
        //    game_controller.Best_time_Text.gameObject.SetActive(false);
        //    game_controller.Close_button.SetActive(true);
        //    game_controller.Resume_button.SetActive(false);
        //    game_controller.To_Main_Menu_button.SetActive(false);
        //    game_controller.disable_pause = true;
        //}
        //to_delay_load = true;
    }

    public void Quit()
    {
        Application.Quit();
        Audio_Manager.Play_SFX_Button_Pressed();
    }

    public void Open_Settings()
    {
        game_controller.Setting_Menu.SetActive(true);
        Audio_Manager.Play_SFX_Button_Pressed();
    }

    void Setting_Buttons_Not_In_Game()
    {
        game_controller.Close_button.SetActive(true);
        game_controller.Resume_button.SetActive(false);
        game_controller.To_Main_Menu_button.SetActive(false);
        //game_controller.disable_pause = true;
    }

    public void Setting_Buttons_In_Game()
    {
        game_controller.Close_button.SetActive(false);
        game_controller.Resume_button.SetActive(true);
        game_controller.To_Main_Menu_button.SetActive(true);
        //game_controller.disable_pause = false;
    }
}
