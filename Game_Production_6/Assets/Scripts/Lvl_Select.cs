using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Lvl_Select : MonoBehaviour
{
    [SerializeField] GameObject L2_Lock;
    [SerializeField] GameObject L3_Lock;
    [SerializeField] GameObject L4_Lock;
    Game_Controller GC;
    void Start()
    {
        GC = FindAnyObjectByType<Game_Controller>();
        if (GC.L2_Locked == false)
            L2_Lock.SetActive(false);
        if (GC.L3_Locked == false)
            L3_Lock.SetActive(false);
        if (GC.L4_Locked == false)
            L4_Lock.SetActive(false);
    }

    void Update()
    {
        
    }
}
