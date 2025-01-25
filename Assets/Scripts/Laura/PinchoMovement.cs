using UnityEngine;

/* 
 * Script que se encarga del movimiento aleatorio del pincho
 */
public class PinchoMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    Vector3 position;
    private void Awake()
    {
        position = transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        position *= speed;

    }
}
