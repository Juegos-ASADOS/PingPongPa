using UnityEngine;

public class PinchoParry : MonoBehaviour
{
    //GameObject Transform
    Transform tr;

    PlayerIDs spikeType;
    [SerializeField]
    int remainingHits;
    [SerializeField]
    Color[] colors = new Color[2];
    //Color spritecolor;
    ColorChangePincho colorCntrl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //TrailAnchors
    Transform trailTransform;
    Transform[] trailsAnchors;
    [SerializeField]
    private Vector3 anchorOffset; 

    private void Awake()
    {
        tr = transform;
        for(int i = 0; i < remainingHits; i++)
        {
            Vector3 anchorPosition = tr.localPosition + anchorOffset * i;
            GameObject anchor = Instantiate(new GameObject());
            anchor.transform.SetParent(tr);
            anchor.transform.localPosition = anchorPosition;
            anchor.name = "TrailAnchor" + i;
        }
    }

    void Start()
    {
        //Transform de la estela
        trailTransform = tr.Find("Trail").transform;

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
        updateTrailAnchor();
        if (remainingHits <= 0)
        {
            Destroy(gameObject);
        }
    }

    void updateTrailAnchor()
    {
        string anchorId = "AnchorTrail" + (remainingHits-1);
        trailTransform = tr.Find(anchorId);
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
        //Playear la animaci�n de muerte explotar
        Destroy(gameObject);
    }
}
