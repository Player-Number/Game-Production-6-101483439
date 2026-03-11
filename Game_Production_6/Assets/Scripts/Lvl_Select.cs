using TMPro;
using UnityEngine;

public class Lvl_Select : MonoBehaviour
{
    [SerializeField] GameObject L2_Lock;
    [SerializeField] GameObject L3_Lock;
    [SerializeField] GameObject L4_Lock;

    [SerializeField] TMP_Text L1_HS_Text;
    [SerializeField] TMP_Text L2_HS_Text;
    [SerializeField] TMP_Text L3_HS_Text;
    [SerializeField] TMP_Text L4_HS_Text;
    Game_Controller GC;
    Extra_Objectives EO;

    public TMP_Text L1_T;
    public TMP_Text L1_C;
    public TMP_Text L1_P;

    public TMP_Text L2_T;
    public TMP_Text L2_S;
    public TMP_Text L2_P;

    public TMP_Text L3_T;
    public TMP_Text L3_S;
    public TMP_Text L3_A;

    public TMP_Text L4_p;
    public TMP_Text L4_c;
    public TMP_Text L4_t;

    void Start()
    {
        GC = FindAnyObjectByType<Game_Controller>();
        EO = FindAnyObjectByType<Extra_Objectives>();
        Lock();
        EO_Text();
        HS();
    }

    void Lock()
    {
        if (GC.L2_Locked == false)
            L2_Lock.SetActive(false);
        if (GC.L3_Locked == false)
            L3_Lock.SetActive(false);
        if (GC.L4_Locked == false)
            L4_Lock.SetActive(false);

    }

    void EO_Text()
    {
        if (EO.B_L1_T)
            L1_T.color = Color.green;
        if (EO.B_L1_C)
            L1_C.color = Color.green;
        if (EO.B_L1_P)
            L1_P.color = Color.green;

        if (EO.B_L2_T)
            L2_T.color = Color.green;
        if (EO.B_L2_S)
            L2_S.color = Color.green;
        if (EO.B_L2_P)
            L2_P.color = Color.green;

        if (EO.B_L3_T)
            L3_T.color = Color.green;
        if (EO.B_L3_S)
            L3_S.color = Color.green;
        if (EO.B_L3_A)
            L3_A.color = Color.green;

        if (EO.B_L4_P)
            L4_p.color = Color.green;
        if (EO.B_L4_C)
            L4_c.color = Color.green;
        if (EO.B_L4_T)
            L4_t.color = Color.green;
    }

    void HS()
    {
        L1_HS_Text.text = "HS: " + GC.L1_HS;
        L2_HS_Text.text = "HS: " + GC.L2_HS;
        L3_HS_Text.text = "HS: " + GC.L3_HS;
        L4_HS_Text.text = "HS: " + GC.L4_HS;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            L2_Lock.SetActive(false);
            L3_Lock.SetActive(false);
            L4_Lock.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            FindAnyObjectByType<Change_Scene>().Scene_To_Load("Lvl_5");
        }
    }
}
