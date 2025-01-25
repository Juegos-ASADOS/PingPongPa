using UnityEngine;
using UnityEngine.InputSystem;

public class ParryObjects : MonoBehaviour
{
    [SerializeField]
    GameObject shield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Parry()
    {
        shield.SetActive(true);
    }
}
