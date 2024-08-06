using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    Image _image;

    [SerializeField] ColorType colorType;

    // Dicionário para mapear ColorType para Color
    private static readonly Dictionary<ColorType, Color> colorMap = new Dictionary<ColorType, Color>
    {
        { ColorType.Bronze, new Color(0.8f, 0.5f, 0.2f) }, // Cor de bronze
        { ColorType.Silver, new Color(0.75f, 0.75f, 0.75f) }, // Cor de prata
        { ColorType.Gold, new Color(1.0f, 0.84f, 0.0f) }, // Cor de ouro
        { ColorType.Mithril, new Color(0.78f, 0.89f, 0.92f) }, // Cor de mithril (tom de azul claro)
        { ColorType.Emerald, new Color(0.31f, 0.78f, 0.47f) } // Cor de esmeralda
    };

    void Start()
    {
        _image = GetComponent<Image>();
        if (colorMap.TryGetValue(colorType, out var color))
        {
            _image.color = color;
        }
        else
        {
            Debug.LogWarning($"ColorType {colorType} não encontrado no dicionário colorMap.");
        }
    }
}