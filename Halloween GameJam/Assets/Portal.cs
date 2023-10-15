using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    public string LevelName;
    
    void Start(){

    }

    void OnTriggerEnter(){
        SceneManager.LoadScene(LevelName);
        Debug.Log("triggered");
    }

}
