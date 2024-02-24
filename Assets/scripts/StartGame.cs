using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Scene_Changer(int Scene_Index)
    {

        SceneManager.LoadScene(Scene_Index);


    }

}
