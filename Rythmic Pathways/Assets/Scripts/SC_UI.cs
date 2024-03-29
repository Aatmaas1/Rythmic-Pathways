using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_UI : MonoBehaviour
{
    public SC_CadenceColl caddy;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        caddy = GameObject.FindWithTag("Player").GetComponent<SC_CadenceColl>();
    }

    // Update is called once per frame
    void Update()
    {
        //hpText.text = caddy.pv.ToString();
        scoreText.text = caddy.score.ToString();
    }
}
