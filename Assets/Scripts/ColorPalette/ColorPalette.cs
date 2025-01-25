using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public Color PlayerColor1 =new Color(0.259f,0.863f,0.737f);
    public Color PlayerColor2 =new Color(1.0f,0.631f,0.773f);

    private Gradient colorGradient;
    private ParticleSystem pSystem;
    private TrailRenderer trailRend;
    private Color initGradientColor= new Color(1.0f, 0.631f, 0);
    void Start()
    {
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0] = new GradientColorKey(initGradientColor, 0);
        colorKeys[1] = new GradientColorKey(PlayerColor2, 0.5f);
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[1];
        alphaKeys[0] = new GradientAlphaKey(1.0f, 0);

        colorGradient = new Gradient();
        colorGradient.SetKeys(colorKeys, alphaKeys);

        pSystem = this.GetComponentInChildren<ParticleSystem>();
        trailRend = this.GetComponent<TrailRenderer>();
        if (trailRend)
        {
            Material mat=trailRend.material;
            if (mat&& mat.HasProperty("_Color")) {
                Debug.Log("Mat property ENCONTRADO");
                mat.SetColor("_Color", PlayerColor2);               
            }       
        }
        if (pSystem)
        {
            Debug.Log("Particles encontradas");
            var colorOverLifetime = pSystem.colorOverLifetime;
            colorOverLifetime.enabled = true;
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(colorGradient);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
