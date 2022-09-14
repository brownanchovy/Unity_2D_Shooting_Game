using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;
    Animator anim;
    Rigidbody2D rigid;
    public CircleCollider2D collision;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float speed;
    public int health;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public float degreePerSecond;



    // Prefab이란 미리 만들어진 Object들을 재활용 가능한 형태로 만들어 두는 것을 의미한다.
    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        //collision = GetComponent<CircleCollider2D>();
        EnemyFire();
        ExtraEnemyFire();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    //problem, the bullet is too many
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        Vector3 curPos = transform.position; 
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    //problem one, the object does not rotate.
    void SwingLeft()
    {
        transform.Rotate(Vector3.left * Time.deltaTime * degreePerSecond);
        transform.Rotate(Vector3.down * Time.deltaTime * degreePerSecond);
    }

    /*
    Quaternion Angle()
    {
        Quaternion angle1 = Quaternion.identity;
        angle1.eulerAngles = new Vector3(30, 30, 30);
        return angle1;
        //angle1 = transform.eulerAngles;
    }
    */

    Vector2 vecfunct1()
    {
        Vector2 vec1 = new Vector2(-1, -3);
        vec1.Normalize();
        return vec1;
    }

    Vector2 vecfunct2()
    {
        Vector2 vec2 = new Vector2( 1, -3);
        vec2.Normalize();
        return vec2;
    }

    void EnemyFire()
    {
        //SwingLeft();
        //Invoke("SwingRight", degreePerSecond);
        //Quaternion angle2 = Angle();
        GameObject bullet = Instantiate(bulletObjA, transform.position + Vector3.down * 0.1f, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.down*10, ForceMode2D.Impulse);
        Invoke("EnemyFire", 0.3f);
    }

    void ExtraEnemyFire()
    {
        Vector2 angvec = vecfunct1();
        Vector2 angvec1 = vecfunct2();
        GameObject bullet1 = Instantiate(bulletObjA, transform.position + Vector3.down * 0.1f , transform.rotation);
        GameObject bullet2 = Instantiate(bulletObjB, transform.position + Vector3.down * 0.1f , transform.rotation);
        Rigidbody2D rigid1 = bullet1.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid2 = bullet2.GetComponent<Rigidbody2D>();
        rigid1.AddForce( angvec * 5, ForceMode2D.Impulse);
        rigid2.AddForce( angvec1 * 5, ForceMode2D.Impulse);
        Invoke("ExtraEnemyFire", 1f);
    }
    /*void Fire()
    {
        //if(!Input.GetButton("Fire1")) //GetButtonDown or Up은 누루는 그 찰나의 순간에 작동한다.
            //return; //이건 프로그래밍 스타일에 따라서 다름
        
        
        switch (enemy)
        {
            case 0: 
                //Power One
                GameObject bullet = Instantiate(bulletObjA, transform.position + Vector3.up * 0.5f, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);
                maxShootDelay = 0.15f;
                break;
            case 1:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up*8, ForceMode2D.Impulse);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up*8, ForceMode2D.Impulse);
                maxShootDelay = 0.3f;
                break;
            case 2:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.25f, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.25f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up*7, ForceMode2D.Impulse);
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up*7, ForceMode2D.Impulse);
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidCC.AddForce(Vector2.up*7, ForceMode2D.Impulse);
                maxShootDelay = 0.45f;
                break;
        }
        //curShootDelay = 0;
    }
    */

    void FixedUpdate()
    {
        
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite() //오타 주의
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
    }
}
