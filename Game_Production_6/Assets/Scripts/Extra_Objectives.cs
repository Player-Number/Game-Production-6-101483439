using UnityEngine;

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

    public bool B_L1_T;
    public bool B_L1_C;
    public bool B_L1_P;
                 
    public bool B_L2_T;
    public bool B_L2_S;
    public bool B_L2_P;

    //public Color O1 = Color.white;
    //public Color O2 = Color.white;
    //public Color O3 = Color.white;
    //void Start()
    //{
        
    //}

    //void Update()
    //{
        
    //}

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

    //public void Set_Completed_EO(string lvl)
    //{
    //    current_lvl = lvl;
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

    public void L1_EO()
    {
        if (FindAnyObjectByType<Timer>().timer >= 15)
            B_L1_T = true;
        if (collectables <= 0)
            B_L1_C = true;
        if (pots <= 0)
            B_L1_P = true;
    }
    public void L2_EO()
    {
        if (two_tar)
            B_L2_T = true;
        if (FindAnyObjectByType<Player>().Score >= 45)
            B_L2_S = true;
        if (pots <= 0)
            B_L2_P = true;
    }
    public void L3_EO()
    {
        
    }

    public void Check_EO()
    {
        if (current_lvl == "Lvl_1")
        {
            L1_EO();
        }
        else if (current_lvl == "Lvl_2")
        {
            L2_EO();
        }
        else if (current_lvl == "Lvl_3")
        {
            L3_EO();
        }
    }
}
