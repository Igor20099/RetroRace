using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int bulletCount;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Text bulletText;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    private int goldArmor = 0;
    Color startColor;
    private int armor = 0;
    // Start is called before the first frame update
    private Bullet bul;

    private bool isGoldArmor;

    public int GoldArmor => goldArmor;
    void Start()
    {
         bul = bullet.GetComponent<Bullet>();
       startColor = spriteRenderers[0].color;
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 4f)
        {
            transform.Translate(Mathf.Floor(3f), 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.A)&& transform.position.x >= -0.2f)
        {
            transform.Translate(Mathf.Floor(-3f), 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.Space) && bulletCount > 0)
        {
            SoundManager.instance.PlayShot();
            GameObject b = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            b.GetComponent<Bullet>().Move();
            bulletCount--;
            bulletText.text = "x" + bulletCount;
        }

        if (bulletCount > 20)
        {
            bulletCount = 20;
            bulletText.text = "x" + bulletCount;
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            SoundManager.instance.PlayPut();
            Destroy(collision.gameObject);
            bulletCount++;
            bulletText.text = "x" + bulletCount;
        }
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Mine"))
        {
            SoundManager.instance.PlayDestroy();

            if (goldArmor == 0)
            {
                if (armor == 1)
                {

                    Destroy(collision.gameObject);
                    armor--;
                    foreach (var sprite in spriteRenderers)
                    {
                        sprite.color = startColor;
                    }
                    GameManager.instanse.AddScore(10);
                }
                else if (armor == 0)
                {


                    foreach (var item in GameObject.FindGameObjectsWithTag(collision.gameObject.tag))
                    {
                        if (item != null)
                            Destroy(item);

                    }



                    this.gameObject.SetActive(false);


                    //GameManager.instanse.SpeedGame = 100;
                    GameManager.instanse.EndGame();
                    bulletCount = 0;
                    bulletText.text = "x" + bulletCount;
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

           else if (goldArmor == 3 || goldArmor == 2)
            {
                isGoldArmor = true;
                Destroy(collision.gameObject);
                goldArmor--;
               
                GameManager.instanse.AddScore(30);
            }
            else if (goldArmor == 1)
            {
                Destroy(collision.gameObject);
                goldArmor--;
               
               
                foreach (var sprite in spriteRenderers)
                {
                    sprite.color = startColor;
                }
                GameManager.instanse.AddScore(30);

            } 
            else if (goldArmor==0 && armor ==0)
            {

                foreach (var item in GameObject.FindGameObjectsWithTag(collision.gameObject.tag))
                {
                    if (item != null)
                        Destroy(item);

                }



                this.gameObject.SetActive(false);


                //GameManager.instanse.SpeedGame = 100;
                GameManager.instanse.EndGame();
                bulletCount = 0;
                bulletText.text = "x" + bulletCount;
            }

            
           
           
        }

        if (collision.gameObject.CompareTag("ArmorEnemy"))
        {
            SoundManager.instance.PlayDestroy();
            if (collision.gameObject.GetComponent<ArmorEnemy>().Health == 1 && goldArmor == 0)
            {
                if (armor == 1)
                {
                    Destroy(collision.gameObject);
                    armor--;
                    foreach (var sprite in spriteRenderers)
                    {
                        sprite.color = startColor;
                    }
                    GameManager.instanse.AddScore(30);
                }
                else if (armor == 0)
                {
                    this.gameObject.SetActive(false);
                    Destroy(collision.gameObject);
                    // GameManager.instanse.SpeedGame = 100;
                    GameManager.instanse.EndGame();
                    bulletCount = 0;
                    bulletText.text = "x" + bulletCount;
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            } else if (collision.gameObject.GetComponent<ArmorEnemy>().Health == 2 && goldArmor == 0)
            {
                armor = 0;
                foreach (var sprite in spriteRenderers)
                {
                    sprite.color = startColor;
                }
                this.gameObject.SetActive(false);
                Destroy(collision.gameObject);
                // GameManager.instanse.SpeedGame = 100;
                GameManager.instanse.EndGame();
                bulletCount = 0;
                bulletText.text = "x" + bulletCount;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

           else if (goldArmor == 3 || goldArmor == 2)
            {
                isGoldArmor = true;
                Destroy(collision.gameObject);
                goldArmor--;

                GameManager.instanse.AddScore(50);
                Debug.Log(goldArmor.ToString());
            }
            else if (goldArmor == 1)
            {
                Destroy(collision.gameObject);
                goldArmor--;


                foreach (var sprite in spriteRenderers)
                {
                    sprite.color = startColor;
                }
                GameManager.instanse.AddScore(50);
                Debug.Log(goldArmor.ToString());

            }
            else if (goldArmor == 0)
            {

                foreach (var item in GameObject.FindGameObjectsWithTag(collision.gameObject.tag))
                {
                    if (item != null)
                        Destroy(item);

                }



                this.gameObject.SetActive(false);


                //GameManager.instanse.SpeedGame = 100;
                GameManager.instanse.EndGame();
                bulletCount = 0;
                bulletText.text = "x" + bulletCount;
            }


        }

        if (collision.gameObject.CompareTag("Armor") && armor == 0 && goldArmor == 0)
        {
            SoundManager.instance.PlayPut();
            foreach (var sprite in spriteRenderers)
            {
                sprite.color = collision.gameObject.GetComponentInChildren<SpriteRenderer>().color;
            }
            Destroy(collision.gameObject);
            armor++;
        } else if((collision.gameObject.CompareTag("Armor") && armor == 1 && goldArmor == 0))
        {
            SoundManager.instance.PlayPut();
            foreach (var sprite in spriteRenderers)
            {
                sprite.color = startColor;   
            }
            Destroy(collision.gameObject);
            armor--;
        }

        if (collision.gameObject.CompareTag("Armor") && goldArmor > 0)
        {
            SoundManager.instance.PlayPut();
            Destroy(collision.gameObject);
            GameManager.instanse.AddScore(10);
        }

        
    }

    public void LeftMove()
    {
        if (transform.position.x >= -0.2f)
        {
            transform.Translate(Mathf.Floor(-3f), 0, 0);
        }
    }
    public void RightMove()
    {
        if (transform.position.x < 4f)
        {
            transform.Translate(Mathf.Floor(3f), 0, 0);
        }
    }

    public void Shoot()
    {
        if (bulletCount > 0)
        {
            SoundManager.instance.PlayShot();
            GameObject b = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
            b.GetComponent<Bullet>().Move();
            bulletCount--;
            bulletText.text = "x" + bulletCount;

        }
    }

    public void AddBullet(int bullet)
    {
        bulletCount += bullet;
        bulletText.text = "x" + bulletCount;
    }

    public void AddGoldArmor()
    {
        foreach (var sprite in spriteRenderers)
        {
            sprite.color = Color.yellow;
        }
        goldArmor = 3;
    }
}
