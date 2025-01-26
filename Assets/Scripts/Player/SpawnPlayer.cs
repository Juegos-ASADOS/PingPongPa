using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public void SpawnPlayerAssoc()
    {
        player.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
