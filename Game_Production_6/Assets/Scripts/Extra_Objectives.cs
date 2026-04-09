using UnityEditor;
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

    public enum EO_Types
    {
        Timer,
        Target,
        Other
    }

    public string current_lvl;
    public float collectables = 0;
    public float pots = 0;
    public bool tar_air = false;
    public bool two_tar = false;
    public bool beat_lvl = false;

    public bool B_L1_T;
    public bool B_L1_C;
    public bool B_L1_P;
                 
    public bool B_L2_T;
    public bool B_L2_S;
    public bool B_L2_P;

    public bool B_L3_T;
    public bool B_L3_S;
    public bool B_L3_A;

    public bool B_L4_P;
    public bool B_L4_C;
    public bool B_L4_T;

    //public Color O1 = Color.white;
    //public Color O2 = Color.white;
    //public Color O3 = Color.white;
    //void Start()
    //{
        
    //}

    //void Update()
    //{
        
    //}

    //public void Get_current_lvl(string Lvl)
    //{
    //    current_lvl = Lvl;
    //    //if (current_lvl == "Lvl_1")
    //    //{
    //    //    collectables = 2;
    //    //    pots = 3;
    //    //}
    //    //else if (current_lvl == "Lvl_2")
    //    //{
    //    //    collectables = 9;
    //    //    pots = 2;
    //    //}
    //    //else if (current_lvl == "Lvl_3")
    //    //{
    //    //    collectables = 9;
    //    //    pots = 9;
    //    //}
    //    //else if (current_lvl == "Lvl_4")
    //    //{
    //    //    collectables = 2;
    //    //    pots = 3;
    //    //}
    //}

    //public void Set_Completed_EO(string Lvl)
    //{
    //    current_lvl = Lvl;
    //    if (current_lvl == "Lvl_1")
    //    {
    //        if (B_L1_T != true)
    //            B_L1_T.color = O1;
    //        if (B_L1_C.color != Color.green)
    //            B_L1_C.color = O2;
    //        if (B_L1_P.color != Color.green)
    //            B_L1_P.color = O3;
    //    }
    //    else if (current_lvl == "Lvl_2")
    //    {
    //        if (B_L2_T.color != Color.green)
    //            B_L2_T.color = O1;
    //        if (B_L2_S.color != Color.green)
    //            B_L2_S.color = O2;
    //        if (B_L2_P.color != Color.green)
    //            B_L2_P.color = O3;
    //    }
    //    //O1 = Color.white;
    //    //O2 = Color.white;
    //    //O3 = Color.white;
    //}

    public void L1_EO(EO_Types t)
    {
        if (FindAnyObjectByType<Timer>().timer >= 15 && t == EO_Types.Timer)
            B_L1_T = true;
        if (collectables >= 2)
            B_L1_C = true;
        if (pots >= 3)
            B_L1_P = true;
    }
    public void L2_EO(EO_Types t)
    {
        if (two_tar)
            B_L2_T = true;
        if (FindAnyObjectByType<Player>().Score >= 45)
            B_L2_S = true;
        if (pots <= 0)
            B_L2_P = true;
    }
    public void L3_EO(EO_Types t)
    {
        Player player = FindAnyObjectByType<Player>();
        if (FindAnyObjectByType<Timer>().timer <= 1 && t == EO_Types.Timer)
            B_L3_T = true;
        if (player.Score >= 25)
            B_L3_S = true;
        if (player.GetComponent<Player_Movement>().is_grounded == false && t == EO_Types.Target)
            B_L3_A = true;
        //if (tar_air)
        //    B_L3_A = true;
    }

    public void L4_EO(EO_Types t)
    {
        if (pots >= 2)
            B_L4_P = true;
        if (collectables >= 3)
            B_L4_C = true;
        if (FindAnyObjectByType<Timer>().timer >= 10 && t == EO_Types.Timer)
            B_L4_T = true;

    }

    public void Check_EO(EO_Types t)
    {
        current_lvl = SceneManager.GetActiveScene().name;
        if (current_lvl == "Lvl_1")
            L1_EO(t);
        else if (current_lvl == "Lvl_2")
            L2_EO(t);
        else if (current_lvl == "Lvl_3")
            L3_EO(t);
        else if (current_lvl == "Lvl_4")
            L4_EO(t);
    }
}
