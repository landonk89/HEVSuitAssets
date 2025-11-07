using UnityEngine;
using UnityEngine.UI;

public class SpriteDesaturate : MonoBehaviour
{
	[Range(0f, 2f)]
	public float exposure = 1.5f;
	public Sprite sourceSprite;

	void Start()
	{
		Image sourceImage = GetComponent<Image>();
		sourceSprite = sourceImage.sprite;
		if (sourceSprite == null) return;

		Texture2D tex = Instantiate(sourceSprite.texture);
		Color[] pixels = tex.GetPixels();

		for (int i = 0; i < pixels.Length; i++)
		{
			// Convert to grayscale and adjust exposure
			Color c = pixels[i];
			float gray = c.r * 0.299f + c.g * 0.587f + c.b * 0.114f;
			gray = Mathf.Lerp(gray, 1f, 0.5f);
			gray *= 1.5f;
			pixels[i] = new Color(gray, gray, gray, c.a);
		}

		tex.SetPixels(pixels);
		tex.Apply();

		// Create a new sprite from the modified texture
		sourceImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
	}
}
