using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableController : MonoBehaviour
{
    [SerializeField]
    private float breakTimer = 0;

    public bool isBroken = false;

    private MeshRenderer meshRend;

    // Start is called before the first frame update
    void Start()
    {
        breakTimer = Random.Range(5, 30);
        meshRend = GetComponent<MeshRenderer>();
        meshRend.material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if(breakTimer > 0)
        {
            breakTimer -= Time.deltaTime;
        }
        else if(!isBroken)
        {
            isBroken = true;
            meshRend.material.color = Color.red;
        }
    }
    public void Fix()
    {
        isBroken = false;
        breakTimer = Random.Range(5, 30);
        meshRend.material.color = Color.green;
    }
}
