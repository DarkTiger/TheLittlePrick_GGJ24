using UnityEngine;


public class Billboard : MonoBehaviour
{


    void Update()
    {
        if (Gamemanager.instance.billBoardEnabled == true)
        {
            transform.LookAt(Camera.main.transform.position);
        }
        
    }
}
