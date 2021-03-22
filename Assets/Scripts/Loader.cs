using UnityEngine;

public class Loader : MonoBehaviour
{
    public static Texture[] hudImage;

    void Start()
    {
        hudImage = new Texture[6];
        hudImage[0] = Resources.Load<Texture>("imgs/patrol");
        hudImage[1] = Resources.Load<Texture>("imgs/sight");
        hudImage[2] = Resources.Load<Texture>("imgs/sound");
        hudImage[3] = Resources.Load<Texture>("imgs/chase");
        hudImage[4] = Resources.Load<Texture>("imgs/confused");
        hudImage[5] = Resources.Load<Texture>("imgs/capture");
    }
}