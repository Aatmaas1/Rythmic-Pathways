using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_CadenceColl : MonoBehaviour
{
    public int pv = 5;
    public int score = 0;
    public bool canColl;
    private void OnCollisionEnter(Collision collision)
    {
        print("Collided");

        if(canColl == true)
        {
            if (collision.gameObject.tag == "LightCube")
            {
                print("yayy");
                score += 1;
               
                Destroy(collision.gameObject);
            }
            else
            {
                pv -= 1;
                print("You suck");
            }

            //canColl = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //canColl = true;
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
