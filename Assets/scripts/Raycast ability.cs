using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastability : MonoBehaviour
{
    public float pointWidth = 1;
    public float pointHeight = 1;
    public float pointLength = 1;
    public bool active;
    public PlayerAbility PA;
    public GameObject Player;


    public Color colorA;


    private new Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        PA = Player.GetComponent<PlayerAbility>();
    }

    // Update is called once per frame
    void Update()
    {

        if (PA.activeAbility == 2 && PA.blockCount < 3)
        {
            colorA = Color.green;
            renderer.material.SetColor("_ColorA", colorA);
            castToBlock();
        }
        if (PA.activeAbility == 3 && PA.blockCount < 3)
        {
            colorA = Color.red;
            renderer.material.SetColor("_ColorA", colorA);
            castToBlock();
        }

    }
    void castToBlock()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == gameObject)
            {
                renderer.material.SetFloat("_Width", pointWidth);
                renderer.material.SetFloat("_Height", pointHeight);
                renderer.material.SetFloat("_Length", pointLength);
                renderer.material.SetVector("_point", new Vector4(hit.point.x, hit.point.y, hit.point.z, 0f));
            }
            else
            {
                renderer.material.SetFloat("_Width", 0f);
                renderer.material.SetFloat("_Length", 0f);
                renderer.material.SetFloat("_Height", 0f);
            }
        }
        else
        {
            renderer.material.SetFloat("_Width", 0f);
            renderer.material.SetFloat("_Length", 0f);
            renderer.material.SetFloat("_Height", 0f);
        }
    }
}