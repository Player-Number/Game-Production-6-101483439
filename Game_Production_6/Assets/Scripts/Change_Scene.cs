using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Change_Scene : MonoBehaviour
{
    Game_Controller Game_Controller;
    Audio_Manager Audio_Manager;
    Extra_Objectives Extra_Objectives;
    //string scene_name;
    //float delay_load_timer = 0.25f;
    //bool to_delay_load = false;

    void Start()
    {
        //Game_Controller = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        //Audio_Manager = GameObject.Find("Audio_Manager").GetComponent<Audio_Manager>();
        Extra_Objectives = FindAnyObjectByType<Extra_Objectives>();
        Game_Controller = FindAnyObjectByType<Game_Controller>();
        Audio_Manager = FindAnyObjectByType<Audio_Manager>();
    }

    void Update()
    {
        //if (to_delay_load)
        //{
        //    delay_load_timer -= Time.deltaTime;
        //    if (delay_load_timer <= 0)
        //    {
        //        SceneManager.LoadScene(name);
        //        if (name != "Main_Menu")
        //        {
        //            Menu.Best_time_Text.gameObject.SetActive(false);
        //        }
        //        else
        //            Menu.Best_time_Text.gameObject.SetActive(true);
        //    }
        //}
    }

    public void Scene_To_Load(string name)
    {
        SceneManager.LoadScene(name);
        //Game_Controller.Check_Scene(name);
        //Extra_Objectives.all_text.transform.localScale = Vector3.zero;
        if (name == "Controls") // name == "How_Play" || 
        {
            Audio_Manager.Play_Music(Audio_Manager.Other_Menu);
            Setting_Buttons_Not_In_Game();
            //Game_Controller.Best_time_Text.gameObject.SetActive(false);
        }
        else if (name == "Main_Menu")
        {
            //Audio_Manager.Stop_Music();
            //Game_Controller.Best_time_Text.gameObject.SetActive(true);
            Time.timeScale = 1;
            Game_Controller.lock_mouse = false;
            Setting_Buttons_Not_In_Game();
            Game_Controller.can_open_setting = true;
            Audio_Manager.Play_Music(Audio_Manager.Main_Menu);
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
        //    Extra_Objectives.all_text.transform.localScale = Vector3.one;

        //    //for (int i = 0; i < Extra_Objectives.all_text.transform.childCount; i++)
        //    //{
        //    //    Debug.Log(i);
        //    //    Extra_Objectives.all_text.transform.GetChild(i).gameObject.SetActive(true);
        //    //}
        //    //Extra_Objectives.all_text.SetActive(true);
        //    //if (Extra_Objectives.L1_T == true)
        //    //    GameObject.Find("L1 15 sec").GetComponent<TMP_Text>().color = Color.gray;
        //    //if (Extra_Objectives.L1_C == true)
        //    //    GameObject.Find("L1 2 spheres").GetComponent<TMP_Text>().color = Color.gray;
        //    //if (Extra_Objectives.L1_P == true)
        //    //{
        //    //    GameObject.Find("L1 3 pots").GetComponent<TMP_Text>().color = Color.gray;
        //    //    Debug.Log("Extra_Objectives.L1_P == true");
        //    //}

        //}
        //if (name != "Lvl_Select")
        //{
        //    Extra_Objectives.all_text.transform.localScale = Vector3.zero;
        //    //for (int i = 0; i < Extra_Objectives.all_text.transform.childCount; i++)
        //    //{
        //    //    Debug.Log(i);
        //    //    Extra_Objectives.all_text.transform.GetChild(i).gameObject.SetActive(false);
        //    //}

        //    //Extra_Objectives.all_text.SetActive(false);

        //}
        //else if (name == "Game_Scene")
        //{
        //    Audio_Manager.Play_Music(Audio_Manager.Gameplay);
        //    //Game_Controller.Best_time_Text.gameObject.SetActive(false);
        //    Game_Controller.lock_mouse = true;
        //    //GameObject.Find("Panel_BG").GetComponent<Image>().material = Game_Controller.Setting_BG_visible;
        //    Setting_Buttons_In_Game();
        //}
        //SceneManager.LoadScene(name);


        //else if (name != "Main_Menu")
        //{
        //    Game_Controller.Best_time_Text.gameObject.SetActive(false);
        //    Game_Controller.Close_button.SetActive(false);
        //    Game_Controller.Resume_button.SetActive(true);
        //    Game_Controller.To_Main_Menu_button.SetActive(true);
        //    Game_Controller.disable_pause = false;
        //}
        //else
        //{
        //    Game_Controller.Best_time_Text.gameObject.SetActive(false);
        //    Game_Controller.Close_button.SetActive(true);
        //    Game_Controller.Resume_button.SetActive(false);
        //    Game_Controller.To_Main_Menu_button.SetActive(false);
        //    Game_Controller.disable_pause = true;
        //}
        //to_delay_load = true;
    }

    public void Quit()
    {
        Audio_Manager.Play_SFX_Button_Pressed();
        Application.Quit();
    }

    public void Open_Settings()
    {
        Game_Controller.Setting_Menu.SetActive(true);
        Audio_Manager.Play_SFX_Button_Pressed();
    }

    void Setting_Buttons_Not_In_Game()
    {
        Game_Controller.Close_button.SetActive(true);
        Game_Controller.Resume_button.SetActive(false);
        Game_Controller.To_Main_Menu_button.SetActive(false);
        //Game_Controller.disable_pause = true;
    }

    public void Setting_Buttons_In_Game()
    {
        Game_Controller.Close_button.SetActive(false);
        Game_Controller.Resume_button.SetActive(true);
        Game_Controller.To_Main_Menu_button.SetActive(true);
        //Game_Controller.disable_pause = false;
    }
}
