using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrinceNpc : MonoBehaviour
{
    private int funnyLevel;
    private float currentLevel;

    [SerializeField] float increasePerSecond;

    public void AddFunnyLevel(int value)
    {
        funnyLevel=value;
    }

    private void Update()
    {
        if (currentLevel<funnyLevel)
        {
            currentLevel += increasePerSecond * Time.deltaTime;
        }
        else if(currentLevel > funnyLevel)
        {
            currentLevel = funnyLevel;
        }
    }
}
