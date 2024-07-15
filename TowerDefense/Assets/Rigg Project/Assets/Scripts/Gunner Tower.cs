using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GunnerTower : MonoBehaviour
{
    private CircleCollider2D Range;
    public List<GameObject> PossibleTargets = new List<GameObject>();
    private GameObject Target;
    public GameObject bullet, range;
    public float ShootTime;
    public int Upgrade = 0;
    private int targetsystem=0;
    public TextMeshProUGUI TargetMessage;

    //Tower UI
    public CanvasTake towerUI;
    bool isTowerUI = true;

    // Start is called before the first frame update
    void Start()
    {
        Range = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(targetsystem);
        if(PossibleTargets.Count != 0 && Target != null)
        {
            transform.right = Target.transform.position - transform.position;
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            // Get the screen position of the mouse click or touch
            Vector3 screenPosition = Input.mousePosition;

            // Convert the screen position to a world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // Perform a raycast at the world position
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            towerUI = hit.collider.gameObject.GetComponent<CanvasTake>();
            if (towerUI != null)
            {
                if(!isTowerUI  && hit.collider.gameObject.tag =="Tower")
                {
                    towerUI.OnActive();
                    print(hit.collider.gameObject.tag);
                    isTowerUI =true;
                }
                else if(isTowerUI &&   hit.collider.gameObject.tag == "Tower")
                {
                    towerUI.OnDisable();
                    isTowerUI=false;
                }
                
            }
            else
            {
                print("null");
                //towerUI.enabled = false;
            }

        }
    }

    IEnumerator StartShooting()
    {
        if (PossibleTargets.Count != 0)
        {
            GameObject Bulle = Instantiate(bullet, transform.position, Quaternion.identity);
            if (Target == null)
            {
                Debug.LogWarning("Bullet destroyed");
                Destroy(Bulle);
            }
            else
            {
                Bulle.GetComponent<Bullet>().Move(Target.transform.position - transform.position);
            }
        }
        yield return new WaitForSeconds(ShootTime);
        StartCoroutine(StartShooting());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (PossibleTargets.Count == 0)
            {
                if (targetsystem == 0) { targetsystem = 3; } else { targetsystem--; }
                TargetSystem();
                StartCoroutine(StartShooting());
            }
            PossibleTargets.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            PossibleTargets.Remove(collision.gameObject);
            if(PossibleTargets.Count == 0)
            {
                StopAllCoroutines();
            }
        }
    }
    public void UpgradeTower()
    {
        switch(Upgrade)
        {
            case 0:
            {
                ShootTime = ShootTime * 0.6f;
                Range.radius *= 1.5f;
                range.transform.localScale *= 1.5f; 
                break;
            }
            case 1: 
            {
                ShootTime = ShootTime * 0.6f;
                Range.radius *= 1.5f;
                range.transform.localScale *= 1.5f;
                bullet.GetComponent<Bullet>().damage = 10f;
                break;
            }
        }
        Upgrade++;
    }
    public void TargetSystem()
    {
        if (targetsystem == 3) { targetsystem = 0; } else { targetsystem++; }
        switch (targetsystem)
        {
            case 0: 
                {
                    StopCoroutine(TargetWeakest());
                    StartCoroutine(TargetFirst());
                    TargetMessage.SetText("Target First");
                    break; 
                }
                
            case 1:
                {
                    StopCoroutine(TargetFirst());
                    StartCoroutine(TargetLast());
                    TargetMessage.SetText("Target Last");
                    break;
                }

            case 2:
                {
                    StopCoroutine(TargetLast());
                    StartCoroutine(TargetStrongest());
                    TargetMessage.SetText("Target Strongest");
                    break;
                }

            case 3:
                {
                    StopCoroutine(TargetStrongest());
                    StartCoroutine(TargetWeakest());
                    TargetMessage.SetText("Target Weakest");
                    break;
                }
        }

    }
    public void DestroyTower()
    {
        Destroy(transform.parent.gameObject);
    }
    IEnumerator TargetFirst()
    {
        float dist = 999f;
        GameObject Temp = null;
        foreach (var target in PossibleTargets)
        {
            if (Vector2.Distance(target.transform.position, transform.position) < dist)
            {
                dist = Vector2.Distance(target.transform.position, transform.position);
                Temp = target;
            }
        }
        Target = Temp;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TargetFirst());
    }
    IEnumerator TargetLast()
    {
        float dist = 0f;
        GameObject Temp = null;
        foreach (var target in PossibleTargets)
        {
            if (Vector2.Distance(target.transform.position, transform.position) > dist)
            {
                dist = Vector2.Distance(target.transform.position, transform.position);
                Temp = target;
            }
        }
        Target = Temp;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TargetLast());
    }
    IEnumerator TargetStrongest()
    {
        float maxhealth = 0f;
        GameObject Temp = null;
        foreach (var target in PossibleTargets)
        {
            if(target.GetComponent<EnemyHealth>().MaxHealth > maxhealth)
            {
                maxhealth = target.GetComponent<EnemyHealth>().MaxHealth;
                Temp = target;
            }
        }
        Target = Temp;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TargetStrongest());
    }
    IEnumerator TargetWeakest() 
    {
        float minhealth = 9999999f;
        GameObject Temp = null;
        foreach (var target in PossibleTargets)
        {
            if(target.GetComponent<EnemyHealth>().MaxHealth < minhealth)
            {
                minhealth = target.GetComponent<EnemyHealth>().MaxHealth;
                Temp = target;
            }
        }
        Target = Temp;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TargetWeakest());
    }
}
