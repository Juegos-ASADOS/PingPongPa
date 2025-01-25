using UnityEngine;

public class PinchoParry : MonoBehaviour
{
    PlayerIDs SpikeType;
    [SerializeField]
    int remainingHits;
    [SerializeField]
    Color[] colors = new Color[2];
    Color spritecolor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                SpikeType = PlayerIDs.PlayerA;
                GetComponent<SpriteRenderer>().color = colors[0];
                break;
            case 1:
                SpikeType = PlayerIDs.PlayerB;
                GetComponent<SpriteRenderer>().color = colors[1];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingHits <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void hit(PlayerIDs p)
    {
        if (p == SpikeType)
        {
            remainingHits--;
            switch (p)
            {
                case PlayerIDs.PlayerA:
                    SpikeType = PlayerIDs.PlayerB;
                    GetComponent<SpriteRenderer>().color = colors[1];
                    break;
                case PlayerIDs.PlayerB:
                    SpikeType = PlayerIDs.PlayerA;
                    GetComponent<SpriteRenderer>().color = colors[0];
                    break;
            }
        }
    }

    public void MainBubbleCollided()
    {
        //Playear la animación de muerte explotar
        Destroy(gameObject);

    }

}
