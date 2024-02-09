using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastability : MonoBehaviour
{
    public float pointRadius = 1;
    public float pointWidth = 1;
    public float pointHeight = 1;
    public float pointLength = 1;
    public bool active;
    public PlayerAbility PA;
    public GameObject Player;


    private new Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        PA = Player.GetComponent<PlayerAbility>();
        Debug.Log(PA);
    }

    // Update is called once per frame
    void Update()
    {
        if (PA.activeAbility == 4)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Ray ray = new Ray(worldPosition, Camera.main.transform.forward);
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            //renderer.material.SetVector("_point", new Vector4(0, 1, 0, 0));

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    renderer.material.SetFloat("_radius", pointRadius);
                    renderer.material.SetFloat("_Width", pointRadius);
                    renderer.material.SetFloat("_Length", pointRadius);
                    renderer.material.SetFloat("_Height", pointRadius);
                    renderer.material.SetVector("_point", new Vector4(hit.point.x, hit.point.y, hit.point.z, 0f));
                    Debug.Log(hit.point);
                }
                else
                {
                    renderer.material.SetFloat("_radius", 0f);
                    renderer.material.SetFloat("_Width", 0f);
                    renderer.material.SetFloat("_Length", 0f);
                    renderer.material.SetFloat("_Height", 0f);
                }
            }
            else
            {
                renderer.material.SetFloat("_radius", 0f);
                renderer.material.SetFloat("_Width", 0f);
                renderer.material.SetFloat("_Length", 0f);
                renderer.material.SetFloat("_Height", 0f);
            }
        }
    }
}