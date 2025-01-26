using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    [SerializeField]
    private float timeToDestruction = 3.0f;
    private float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (time < timeToDestruction) time += Time.deltaTime;
        else Destroy(gameObject);
    }
}
