using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public PlayerAbility playerAbility;
    public Color defaultColor = Color.magenta;
    public Color ability1Color = Color.yellow;
    public Color ability2Color = Color.green;
    public Color ability3Color = Color.red;


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


    }
}
