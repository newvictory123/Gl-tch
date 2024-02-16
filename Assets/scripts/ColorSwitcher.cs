using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public PlayerAbility playerAbility;
    public Color defaultColor = new Color(38, 0,77, 1);
    public Color ability1Color = Color.yellow;
    public Color ability2Color = Color.green;
    public Color ability3Color = Color.red;
    public Color ability4Color = Color.white;


    private new Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (playerAbility.activeAbility == 0)
        {
            renderer.material.SetColor("_Glitch_color", defaultColor);
        }
        if (playerAbility.activeAbility == 1)
        {
            renderer.material.SetColor("_Glitch_color", ability1Color);
        }
        if (playerAbility.activeAbility == 2)
        {
            renderer.material.SetColor("_Glitch_color", ability2Color);
        }
        if (playerAbility.activeAbility == 3)
        {
            renderer.material.SetColor("_Glitch_color", ability3Color);
        }
        if (playerAbility.activeAbility == 4)
        {
            renderer.material.SetColor("_Glitch_color", ability4Color);
        }

    }
}
