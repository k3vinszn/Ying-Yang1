using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{
    // tou a ir buscar o script que esta no game object

    [SerializeField] DoorBehavior doorBehaviour;


    [SerializeField] bool isDoorOpenSwitch;
    [SerializeField] bool isDoorClosedSwitch;

    float switchSizeY;
    Vector3 switchUpPos;
    Vector3 switchDownPos;
    float switchSpeed= 1f; // velocide em que o switch e pressionado
    float swichDelay= 0.2f; // velocidade em que o switch volta a subir
    bool PressingSwitch = false; // checks se o switch foi pressionado, qndo o jogo comeca vai tar no false


    void Awake()
    {
        switchSizeY = transform.localScale.y / 2;
        switchUpPos = transform.position;
        switchDownPos = new Vector3(transform.position.x, transform.position.y - switchSizeY, transform.position.z);
        PressingSwitch = false;
    }

    void Update()
    {

        if (PressingSwitch)
        {
            MoveSwitchDown();
        }
        else if (!PressingSwitch)
        {
            MoveSwitchUp();
        }
    }
    void MoveSwitchDown()
    {
        if (transform.position != switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchUpPos, switchSpeed * Time.deltaTime);
        }
    }

    void MoveSwitchUp()
    {
        if (transform.position != switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchDownPos, switchSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PressingSwitch = !PressingSwitch; // checa se e falso ou true

            if (isDoorOpenSwitch && !doorBehaviour.isDoorOpen)
            {
                doorBehaviour.isDoorOpen = !doorBehaviour.isDoorOpen;
            }
            else if(isDoorClosedSwitch && !doorBehaviour.isDoorOpen)
            {
                doorBehaviour.isDoorOpen = !doorBehaviour.isDoorOpen;
            }
        }

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchUpDelay(swichDelay));
            Debug.Log("Player saiu de cima do trigger");

            if (isDoorClosedSwitch && doorBehaviour.isDoorOpen)
            {
                doorBehaviour.isDoorOpen = false;
            }
        }


    }
   

    IEnumerator SwitchUpDelay(float waitTime)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(waitTime);
        PressingSwitch = true;
    }

}
