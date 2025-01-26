using UnityEngine;

public class ColorChangePincho : MonoBehaviour
{
    public Color PlayerColor1 =new Color(0.259f,0.863f,0.737f);
    public Color PlayerColor2 =new Color(1.0f,0.631f,0.773f);
    private Color initGradientColor= new Color(1.0f, 0.631f, 0);

    private Gradient colorGradient;
    private ParticleSystem pSystem;
    private TrailRenderer trailRend;
    private SpriteRenderer spriteEscudo1;
    private readonly SpriteRenderer spriteEscudo2;
    SpriteRenderer[] _renderer;
    void Awake()
    {

        pSystem = this.GetComponentInChildren<ParticleSystem>();
        trailRend = this.GetComponent<TrailRenderer>();
        _renderer = GetComponentsInChildren<SpriteRenderer>();


    }
    public void ChangeColorPincho(int player)
    {
        Color colorPincho;
        //eleccion de color por player
        if (player == 0) colorPincho = PlayerColor1;
        else colorPincho = PlayerColor2;
        
        if (trailRend)
        {
            //cambiar color del trail
            Material mat = trailRend.material;
            if (mat && mat.HasProperty("_Color"))
            {
                mat.SetColor("_Color", colorPincho);
            }
        }
        if (pSystem)
        {
            //gradiente para la particle System
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0] = new GradientColorKey(initGradientColor, 0);
            colorKeys[1] = new GradientColorKey(colorPincho, 0.5f);
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[1];
            alphaKeys[0] = new GradientAlphaKey(1.0f, 0);

            colorGradient = new Gradient();
            colorGradient.SetKeys(colorKeys, alphaKeys);
            //setear gradiente a las particulas
            var colorOverLifetime = pSystem.colorOverLifetime;
            colorOverLifetime.enabled = true;
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(colorGradient);
        }
        foreach(var render in _renderer)
        {
            Material mat = render.material;
            if (mat && mat.HasProperty("_Color"))
            {
                mat.SetColor("_Color", colorPincho);
            }
        }
    }

 
}
