using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ParryObjects : MonoBehaviour
{
    [SerializeField]
    GameObject shield;

    public void Parry(CallbackContext context)
    {
        if (context.started)        //Comprobar si el boton se acaba de pulsar
        {            
            shield.SetActive(true);
            Debug.Log("HOLO");
        }
    }
}
