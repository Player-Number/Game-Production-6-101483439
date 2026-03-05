using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] Player Player;
    [SerializeField] float max_FB_in_effect_timer; // 2
    public float FB_in_effect_timer = 0;
    public float timer = 5;
    Image FB_effect;
    bool in_FB = false;
    void Start()
    {
        FB_effect = FindAnyObjectByType<Player>().flash_bang.GetComponent<Image>();
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

        if (FB_in_effect_timer > 1)
        {
            //FB_effect.gameObject.SetActive(true);
            FB_effect.color = new Color(FB_effect.color.r, FB_effect.color.g, FB_effect.color.b, 1);
            FB_in_effect_timer -= Time.deltaTime;
            in_FB = true;
        }
        else if (in_FB && FB_in_effect_timer < 1)
        {
            StartCoroutine(Fade_Out());
        }
    }
    [SerializeField] float fade_duration;
    private IEnumerator Fade_Out()
    {
        in_FB = false;
        for (float i = fade_duration; i >= 0; i -= Time.deltaTime)
        {
            FB_effect.color = new Color(FB_effect.color.r, FB_effect.color.g, FB_effect.color.b, Mathf.Clamp01(i / fade_duration));
            yield return null;
        }
        FB_effect.color = new Color(FB_effect.color.r, FB_effect.color.g, FB_effect.color.b, 0f);
    }

    public void How_Play_Lvl_Timer()
    {
        timer = 999;
    }
}
