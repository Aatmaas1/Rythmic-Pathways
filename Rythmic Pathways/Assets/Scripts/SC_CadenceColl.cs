using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_CadenceColl : MonoBehaviour
{
    int pv = 5;
    public int score = 0;
    public int tileScore;
    bool isColl;
    private void OnCollisionEnter(Collision collision)
    {
        print("Collided");

            if (collision.gameObject.tag == "LightCube")
            {
                isColl = false;
                print("yayy");
            }
            else if(isColl == true)
            {
                pv -= 1;
                print("You suck");
            }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "LightCube")
        {
            print("yayy");
            score += tileScore;
            isColl = true;
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if(pv<= 0)
        {
            int scene = SceneManager.GetActiveScene().buildIndex; 
            SceneManager.LoadScene(scene);
        }
    }
}
