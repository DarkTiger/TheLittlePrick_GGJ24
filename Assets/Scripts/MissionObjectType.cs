using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionObjectEventType
{
    Bag,
    Lamp,
    Fax,
    Jokes
}

public class MissionObjectType : MonoBehaviour
{
    [SerializeField] MissionObjectEventType eventType;

    public MissionObjectEventType EventType()
    {
        return eventType;
    }
}
