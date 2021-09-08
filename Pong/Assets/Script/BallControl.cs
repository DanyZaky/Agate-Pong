using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    public float ballSpeed;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Mulai game
        RestartGame();

        trajectoryOrigin = transform.position;
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        if (randomDirection < 1.0f)
        {
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce).normalized, ForceMode2D.Impulse);
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce).normalized, ForceMode2D.Impulse);
        }

        //Menggunakan Normalized terlebih dahulu agar sebuah vektor2 mempertahankan arah yang sama tetapi panjangnya bernilai default atau yg ditetapkan

        //Lalu menambah parameter ForceMode2D.Impulse untuk menerapkan nilai impuls secara instan. Syntax ini tergantung pada massa benda tegar sehingga lebih banyak gaya yang harus diterapkan
        //untuk memindahkan benda bermassa lebih tinggi dengan jumlah yang sama dengan benda bermassa lebih rendah.

    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
