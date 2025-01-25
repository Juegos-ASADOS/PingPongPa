using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ParryObjects : MonoBehaviour
{
    [SerializeField]
    GameObject shield;
    [SerializeField]
    float cooldown;
    private float timeToCooldown;

    private void Update()
    {
        if (timeToCooldown > 0) 
            timeToCooldown -= Time.deltaTime;
    }

    public void Parry(CallbackContext context)
    {
        if (timeToCooldown <= 0 && context.started)        //Comprobar si el boton se acaba de pulsar
        {
            shield.SetActive(true);
            timeToCooldown = cooldown;
        }
    }
}
