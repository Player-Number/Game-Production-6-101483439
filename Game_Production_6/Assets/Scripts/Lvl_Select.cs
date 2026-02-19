using TMPro;
using UnityEngine;

public class Lvl_Select : MonoBehaviour
{
    [SerializeField] GameObject L2_Lock;
    [SerializeField] GameObject L3_Lock;
    [SerializeField] GameObject L4_Lock;
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
    public TMP_Text L3_P;

    void Start()
    {
        GC = FindAnyObjectByType<Game_Controller>();
        EO = FindAnyObjectByType<Extra_Objectives>();
        if (GC.L2_Locked == false)
            L2_Lock.SetActive(false);
        if (GC.L3_Locked == false)
            L3_Lock.SetActive(false);
        if (GC.L4_Locked == false)
            L4_Lock.SetActive(false);

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
    }

    void Update()
    {
        
    }
}
