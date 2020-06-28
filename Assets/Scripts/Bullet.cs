using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Score score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 16f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Move()
    {
        if (this.gameObject.active)
        StartCoroutine(DirMove());
    }

    IEnumerator DirMove()
    {
        while (true)
        {
            transform.position += Vector3.up * 3f;
            yield return new WaitForSeconds(0.2f * GameManager.instanse.SpeedGame) ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.PlayDestroy();
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            GameManager.instanse.AddScore(score.ScoreCount);
        }

        if(collision.gameObject.CompareTag("Obstacle") )
            {
            transform.position = Vector3.zero;
            Destroy(this.gameObject);
        }
    }
    
}
