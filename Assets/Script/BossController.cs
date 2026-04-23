using UnityEngine;
using System.Collections;


public class BossController : MonoBehaviour
{
    public float survivalTime = 10f; 
    public float moveSpeed = 3f;     
    public Transform player;         
    public GameObject goalObject;    

    private bool isChasing = false;
    private Animator bossAni;


    void Start()
    {
        StartBossBattle();
    }
    void Awake()
    {
        bossAni = GetComponent<Animator>();

        if (goalObject != null)
            goalObject.SetActive(false);
    }

    public void StartBossBattle()
    {
        StartCoroutine(BossSequence());
    }

    IEnumerator BossSequence()
    {
        bossAni.SetTrigger("Transform");
        yield return new WaitForSeconds(2f);

        isChasing = true;

        yield return new WaitForSeconds(survivalTime);

        isChasing = false;

        bossAni.SetTrigger("Die");
        yield return new WaitForSeconds(2f);
        Debug.Log("토끼가 지쳐 쓰러졌다!"); 

        
        if (goalObject != null)
        {
            goalObject.SetActive(true);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (isChasing && player != null)
        {

            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            
            if (direction.x > 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (direction.x < 0) transform.localScale = new Vector3(1, 1, 1);
        }
    }
}