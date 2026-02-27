using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] Player Player;
    [SerializeField] float max_FB_in_effect_timer; // 2
    public float FB_in_effect_timer = 0;
    public float timer = 5;
    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        Timer_Text.text = timer.ToString("F2");
        if (timer <= 0)
        {
            Player.Lose_Screen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Timer_Text.text = "00.00";
        }

        if (FB_in_effect_timer > 0)
        {
            FindAnyObjectByType<Player>().flash_bang.SetActive(true);
            FB_in_effect_timer -= Time.deltaTime;
        }
        else
        {
            FindAnyObjectByType<Player>().flash_bang.SetActive(false);
        }
        //Debug.Log(FB_in_effect_timer);
    }
}
