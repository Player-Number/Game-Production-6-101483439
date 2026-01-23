using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text Timer_Text;
    [SerializeField] Player Player;
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
    }
}
