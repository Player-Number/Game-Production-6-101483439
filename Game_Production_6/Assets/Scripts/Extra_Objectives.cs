using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Extra_Objectives : MonoBehaviour
{
    public static Extra_Objectives Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject all_text;

    public string current_lvl;
    public float collectables = 0;
    public float pots = 0;

    public TMP_Text L1_T;
    public TMP_Text L1_C;
    public TMP_Text L1_P;
    void Start()
    {
        all_text.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        
    }

    public void Get_current_lvl(string lvl)
    {
        current_lvl = lvl;
        if (current_lvl == "Lvl_1")
        {
            collectables = 2;
            pots = 3;
            FindAnyObjectByType<Timer>().timer = 30;
        }
    }
}
