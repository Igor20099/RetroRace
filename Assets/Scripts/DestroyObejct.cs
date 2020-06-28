using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObejct : MonoBehaviour
{
    [SerializeField] private Score score;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -8.6f)
        {
            Destroy(this.gameObject);
            if (!this.gameObject.CompareTag("Bullet"))
                GameManager.instanse.AddScore(score.ScoreCount);
        }
    }
}
