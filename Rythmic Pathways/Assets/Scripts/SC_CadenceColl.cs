using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_CadenceColl : MonoBehaviour
{
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
}
