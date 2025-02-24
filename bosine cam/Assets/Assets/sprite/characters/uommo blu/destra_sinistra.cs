using UnityEngine;

public class destra_sinistra : MonoBehaviour
{


    public bool isFacingRight = true;
    public Transform cavaliere;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cavaliere.transform.position.x > 5.6f && isFacingRight==false)
        {
            Flip();
            isFacingRight=true;
        } 
        if(cavaliere.transform.position.x < 5.6f && isFacingRight==true)
        {
            Flip();
            isFacingRight=false;
        }
    }

    private void Flip()
    {
        transform.Rotate (0.0f, 180.0f, 0.0f);
    }
}
