using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Color PlayerColor1 =new Color(0.259f,0.863f,0.737f);
    public Color PlayerColor2 =new Color(1.0f,0.631f,0.773f);
    [SerializeField]
   
    private ParticleSystem pSystem;
    private TrailRenderer trailRend;
    void Start()
    {
       
        pSystem = this.GetComponentInChildren<ParticleSystem>();
        trailRend = this.GetComponent<TrailRenderer>();
        if (trailRend)
        {
            Debug.Log("Trail ENCONTRADO");
            Material mat=trailRend.material;

            if (mat) {
                Debug.Log("Mat ENCONTRADO"+mat.name);
                if (mat.HasProperty("_Color"))
                {
                    Debug.Log("Mat property ENCONTRADO");
                    mat.SetColor("_Color", PlayerColor2);
                }
            }
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
