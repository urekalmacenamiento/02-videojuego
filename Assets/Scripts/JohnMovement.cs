using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{   //Public significa que se puede ver en el programa directamen
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    //crear variable global tipo bool true o false
    private bool Grounded;
    //crear solo una bala
    private float LastShoot;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento
        Horizontal = Input.GetAxisRaw("Horizontal");

        if(Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Animator.SetBool("running", Horizontal != 0.0f);
        //Es para ver el resultado raycast
        Debug.DrawRay(transform.position,Vector3.down * 0.1f, Color.red);

        //raycast lanza rayo hacia abajo, devuelve true o false para bri 1
        if(Physics2D.Raycast(transform.position,Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        //Salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if(Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

        private void Jump()
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
        }
        private void Shoot()
        {   
            Vector3 direction;
            if(transform.localScale.x == 1.0f) direction = Vector2.right;
            else direction = Vector2.left;
            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

}
