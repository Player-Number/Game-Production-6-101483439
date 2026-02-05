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
    public bool two_tar = false;

    public TMP_Text L1_T;
    public TMP_Text L1_C;
    public TMP_Text L1_P;

    public TMP_Text L2_T;
    public TMP_Text L2_S;
    public TMP_Text L2_P;

    public Color O1 = Color.white;
    public Color O2 = Color.white;
    public Color O3 = Color.white;
    void Start()
    {
        
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
        }
        else if (current_lvl == "Lvl_2")
        {
            collectables = 1;
            pots = 2;
        }
        else if (current_lvl == "Lvl_3")
        {
            collectables = 1;
            pots = 1;
        }
    }

    public void Set_Completed_EO(string lvl)
    {
        current_lvl = lvl;
        if (current_lvl == "Lvl_1")
        {
            if (L1_T.color != Color.green)
                L1_T.color = O1;
            if (L1_C.color != Color.green)
                L1_C.color = O2;
            if (L1_P.color != Color.green)
                L1_P.color = O3;
        }
        else if (current_lvl == "Lvl_2")
        {
            if (L2_T.color != Color.green)
                L2_T.color = O1;
            if (L2_S.color != Color.green)
                L2_S.color = O2;
            if (L2_P.color != Color.green)
                L2_P.color = O3;
        }

        O1 = Color.white;
        O2 = Color.white;
        O3 = Color.white;
    }

    public void L1_EO()
    {
        if (two_tar)
            O1 = Color.green;
        if (collectables <= 0)
            O2 = Color.green;
        if (pots <= 0)
            O3 = Color.green;
    }
    public void L2_EO()
    {
        if (FindAnyObjectByType<Timer>().timer >= 15)
            O1 = Color.green;
        if (FindAnyObjectByType<Player>().Score >= 60)
            O2 = Color.green;
        if (pots <= 0)
            O3 = Color.green;
    }

    public void Check_EO()
    {
        L1_EO();
        L2_EO();
    }
}
