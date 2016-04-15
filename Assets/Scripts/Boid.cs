using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour {

    public int numberOfBoids = 25;
    public GameObject BoidPrefab;

    private GameObject[] Boids;

	// Use this for initialization
	void Start () {
        this.Boids = new GameObject[numberOfBoids];

        for (int i= 0; i< this.numberOfBoids; i++)
        {
            this.Boids[i] = (GameObject)Instantiate(this.BoidPrefab, this.transform.position + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f)), new Quaternion() );
        }
	}

    Vector3 Cohesion(GameObject pBoid)
    {
        Vector3 centerOfMass = new Vector3();

        foreach (GameObject Boid in this.Boids)
        {
            if (Boid != pBoid)
            {
                centerOfMass += Boid.transform.position;
            }
        }

        centerOfMass /= this.Boids.Length - 1;

        return (centerOfMass - pBoid.transform.position);
    }

    Vector3 Separation(GameObject pBoid)
    {

        Vector3 vec = new Vector3();

        foreach (GameObject Boid in this.Boids)
        {
            if (Boid != pBoid)
            {
                if ( Vector3.Distance(Boid.transform.position, pBoid.transform.position) < 0.3 )
                {
                    vec = vec - (Boid.transform.position - pBoid.transform.position);
                }
            }
        }

        return vec*100;
    }

    Vector3 Alignment(GameObject pBoid)
    {
        Vector3 pc = new Vector3();

        foreach (GameObject Boid in this.Boids)
        {
            if (Boid != pBoid)
            {
                pc += Boid.GetComponent<Rigidbody>().velocity;
            }
        }

        pc /= this.Boids.Length - 1;

        return (pc - pBoid.GetComponent<Rigidbody>().velocity) /8 ;
    }

	// Update is called once per frame
	void FixedUpdate () {

        //Iterate through all boids, calculate and apply velocity!
        foreach (GameObject boid in this.Boids)
        {
            //Apply Velocity
            boid.GetComponent<Rigidbody>().velocity += (Separation(boid) + Cohesion(boid) + Alignment(boid));
        }
	}
}
