using UnityEngine;

public class PinchoParry : MonoBehaviour
{
    PlayerIDs spikeType;
    [SerializeField]
    int remainingHits;
    [SerializeField]
    Color[] colors = new Color[2];
    //Color spritecolor;
    ColorChangePincho colorCntrl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorCntrl = GetComponent<ColorChangePincho>();
        switch (Random.Range(0, 2))
        {
            case 0:
                spikeType = PlayerIDs.PlayerA;
                break;
            case 1:
                spikeType = PlayerIDs.PlayerB;                
                break;
        }
        colorCntrl.ChangeColorPincho((int)spikeType - 1);
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
        if (p == spikeType)
        {
            remainingHits--;
            switch (p)
            {
                case PlayerIDs.PlayerA:
                    spikeType = PlayerIDs.PlayerB;
                    break;
                case PlayerIDs.PlayerB:
                    spikeType = PlayerIDs.PlayerA;
                    break;
            }
        }
        colorCntrl.ChangeColorPincho((int)spikeType - 1);
    }

    public void MainBubbleCollided()
    {
        //Playear la animación de muerte explotar
        Destroy(gameObject);
    }
}
