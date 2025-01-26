using UnityEngine;

public class SpikeAnimation : MonoBehaviour
{
    private GameObject spikeSpawner;
    public void Init(GameObject spawner)
    {
        spikeSpawner = spawner;
    }
    public void spawnSpike()
    {
        spikeSpawner.GetComponent<PinchoSpawner>().SpawnSpike();
    }
}
