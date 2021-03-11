using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baris : MonoBehaviour
{
    [SerializeField]
    private int barisInt;
    public int gotalpbabaUmutbaba;
    public Rigidbody rb;

    private void Awake()
    {
        rb = transform.FindChild("child").GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newfunc();
    }

    void newfunc()
    {
        Debug.Log(Time.deltaTime);
    }
}
