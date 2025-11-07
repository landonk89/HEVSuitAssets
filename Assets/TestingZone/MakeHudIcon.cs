using UnityEngine;
using UnityEngine.UI;

public class MakeHudIcon : MonoBehaviour
{
    private Image hudImage;

    void Start()
    {
        hudImage = GetComponent<Image>();
        hudImage.sprite = CreateSilhouetteSprite(hudImage.sprite);
        hudImage.color = new(1f, 0.7f, 0f, 0.7f); // Orange color with some transparency
    }

    public static Sprite CreateSilhouetteSprite(Sprite source)
    {
        if (source == null)
        {
            Debug.LogError("CreateSilhouetteSprite() Source sprite is null!");
            return null;
        }

        Rect rect = source.textureRect;
        Color[] pixels = source.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        Texture2D silhouetteTex = new((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);
        for (int i = 0; i < pixels.Length; i++)
        {
            float alpha = pixels[i].a;
            pixels[i] = new Color(1f, 1f, 1f, alpha); // White color, preserve alpha
        }

        silhouetteTex.SetPixels(pixels);
        silhouetteTex.Apply();
        return Sprite.Create(silhouetteTex, new Rect(0, 0, silhouetteTex.width, silhouetteTex.height), source.rect.size, source.pixelsPerUnit);
    }
}
