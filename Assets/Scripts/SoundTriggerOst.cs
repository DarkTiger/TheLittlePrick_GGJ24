using UnityEngine;
using UnityEngine.Events;

public class SoundTriggerOst : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;
    //[SerializeField] private bool firstTime = true;
    //[SerializeField] private bool isOst;

    private void Start()
    {
        if (GetComponent<AudioSource>().playOnAwake)
        {
            AudioManager.instance.SetActiveOst(gameObject);

            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            onTriggerEnterEvent.Invoke();



            GetComponent<AudioSource>().Play();
            //firstTime = false;

            GetComponent<AudioSource>().UnPause();
            GetComponent<BoxCollider>().enabled = false;

            AudioManager.instance.SetActiveOst(gameObject);
        }
    }

    public void ResetVariables()
    {

        GameObject activeOst= AudioManager.instance.GetActiveOst();

        activeOst.GetComponent<BoxCollider>().enabled = true;
        activeOst.GetComponent <AudioSource>().Stop();
        

        
        
    }
}
