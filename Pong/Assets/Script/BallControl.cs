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

    void Update()
    {
        //Jika nilai rigidbody2d.velocity.magnitude (BallSpeed) tidak sama dengan 10, maka nilai rigidbody2d.velocity.magnitude (BallSpeed) nilainya 10

        //Memanggil normalisasi agak memakan waktu (perlu akar kuadrat) tetapi Anda tidak akan melihat masalah apa pun jika Anda tidak menjalankan ribuan
        //instance objek ini (dan saya kira fungsi ClampMagnitude tetap membutuhkan akar kuadrat dan akan tidak lebih efisien)

        if (rigidBody2D.velocity.sqrMagnitude != ballSpeed * ballSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * ballSpeed;
        }
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
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }

        //Tugas: Saya memaksakan nilai dari rigidbody2d.velocity.magnitude (BallSpeed) menjadi konstan dengan menerapkan pada method
        // void Update
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
