using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    int dnaLength = 2;
    public float timeAlive;
    public float timeWalking;
    public DNA dna;
    public GameObject eyes;
    bool alive = true;
    bool seeGround = true;

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Dead")
        {
            alive = false;
            timeWalking = 0;
            timeAlive = 0;
        }
    }

    public void Init()
    {
        //init DNA
        //0 forward
        //1 left
        //2 right
        dna = new DNA(dnaLength, 3);
        timeAlive = 0;
        alive = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.blue, 3);
        seeGround = false;
        RaycastHit hit;

        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit))
        {
            if (hit.collider.gameObject.tag == "Platform")
            {
                seeGround = true;
            }
        }

        timeAlive = PopulationManager.elapsed;

        //read DNA
        float turn = 0;
        float move = 0;

        if (seeGround)
        {
            if (dna.GetGene(0) == 0) { move = 1; timeWalking += 1; }
            else if (dna.GetGene(0) == 1) turn = -90;
            else if (dna.GetGene(0) == 2) turn = 90;
        }
        else
        {
            if (dna.GetGene(1) == 0) { move = 1; timeWalking += 1; }
            else if (dna.GetGene(1) == 1) turn = -90;
            else if (dna.GetGene(1) == 2) turn = 90;
        }

        this.transform.Rotate(0, turn, 0);
        this.transform.Translate(0, 0, move * 0.1f);
    }
}
