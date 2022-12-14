using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//Rigidbody kenitic은 Dynamic과 다르게 충돌시 물리연산을 무시한다는 뜻이다.

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public float speed;
    public float power;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    Animator animator;
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public float maxShootDelay;
    public float curShootDelay;
    //public float curShootDelay_Default;
    //public float maxShootDelay_Default;

    //public인 speed 변수는 unity 창에 뜬다.
    void Awake() {
        animator = GetComponent<Animator>();
        power = 1;
        //maxShootDelay_Default = 1f;

        DefaultFire();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //DefaultFire();
        Fire();
        Reload();

    }

    void Reload()
    {
        curShootDelay += Time.deltaTime;
        //curShootDelay_Default += Time.deltaTime;
    }

    void Move() //캡슐화
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h==1) || (isTouchLeft && h== -1)){
            h = 0; //player가 border에 닿았으며, GetAsixRaw이므로 h,v 값이 버튼을 받는다면 1이다
        }

        float v = Input.GetAxisRaw("Vertical");
        if((isTouchTop && v==1) || (isTouchBottom && v== -1)){
            v= 0;
        }
        Vector3 curPos = transform.position; 
        Vector3 nextPos = new Vector3(h,v, 0) * speed * Time.deltaTime;
        //transform 이동에는 Time.deltaTime을 사용하기

        transform.position = curPos + nextPos;
        // 현재위치 + 입력받은_다음위치가 다시 현재위치가 되는 공식

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal")){
            //SetInteger()는 anim이 Input 값을 받음
            animator.SetInteger("Input", (int)h); //강제 자료형 변환
            //SetInteg9er는 Animator.SetInterger에서 나옴
        }
    }

    void DefaultFire()
    {

        //if(curShootDelay < maxShootDelay)
        //{
            GameObject bulletL0 = Instantiate(bulletObjA, transform.position + Vector3.left * 0.5f, transform.rotation);
            Rigidbody2D rigidL0 = bulletL0.GetComponent<Rigidbody2D>();
            rigidL0.AddForce(Vector2.up*10, ForceMode2D.Impulse);
            GameObject bulletR0 = Instantiate(bulletObjA, transform.position + Vector3.right * 0.5f, transform.rotation);
            Rigidbody2D rigidR0 = bulletR0.GetComponent<Rigidbody2D>();
            rigidR0.AddForce(Vector2.up*10, ForceMode2D.Impulse);
        //}
        //curShootDelay_Default = 0;
        Invoke("DefaultFire", 0.5f);
    }

    void Fire()
    {
        if(!Input.GetButton("Fire1")) //GetButtonDown or Up은 누루는 그 찰나의 순간에 작동한다.
            return; //이건 프로그래밍 스타일에 따라서 다름
        if(curShootDelay < maxShootDelay)
            return;
            
        
        switch (power)
        {
            case 1: 
                //Power One
                GameObject bullet = Instantiate(bulletObjA, transform.position + Vector3.up * 0.5f, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);
                maxShootDelay = 0.15f;
                break;
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up*8, ForceMode2D.Impulse);
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up*8, ForceMode2D.Impulse);
                maxShootDelay = 0.3f;
                break;
            case 3:
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
        curShootDelay = 0;
    }

    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Enemy_Bullet")
        {
            gameManager.health -- ;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Border"){
            switch(collision.gameObject.name){
                case "1":
                    isTouchTop = true;
                    break;
                case "2":
                    isTouchBottom = true;
                    break;
                case "3":
                    isTouchLeft = true;
                    break;
                case "4":
                    isTouchRight = true;
                    break;
                    }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Border"){
            switch(collision.gameObject.name){
                case "1":
                    isTouchTop = false;
                    break;
                case "2":
                    isTouchBottom = false;
                    break;
                case "3":
                    isTouchLeft = false;
                    break;
                case "4":
                    isTouchRight = false;
                    break;
            }
        }
    }
}
