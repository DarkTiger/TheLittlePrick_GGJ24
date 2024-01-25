using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    [Header("Mission Object")]
    [SerializeField] Image missionObjectImage;
    [SerializeField] TextMeshProUGUI missionObjectText;

    [Header("Funny Object")]
    [SerializeField] Image funnyObjectImage;
    [SerializeField] TextMeshProUGUI funnyObjectText;

    [SerializeField]GameObject playerInventory;


}
