using UnityEngine;

public enum players { PlayerA, PlayerB }
public class PinchoParry : MonoBehaviour
{
    players SpikeType;
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
                SpikeType = players.PlayerA;
                GetComponent<SpriteRenderer>().color = colors[0];
                break;
            case 1:
                SpikeType = players.PlayerB;
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
    public void hit(players p)
    {
        if (p == SpikeType)
        {
            remainingHits--;
            switch (p)
            {
                case players.PlayerA:
                    p = players.PlayerB;
                    GetComponent<SpriteRenderer>().color = colors[1];
                    break;
                case players.PlayerB:
                    p = players.PlayerA;
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
