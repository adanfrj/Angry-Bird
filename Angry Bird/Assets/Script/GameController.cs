using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    private bool isGameEnded = false;

    public TrailController TrailController;

    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird shotBird;
    public BoxCollider2D TapCollider;

    
    public GameObject Panel;

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        //apabila burung 1 sudah dilempar/hancur, maka load burung 2 buat dilempar
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        //apabila enemy habis maka cek kondisi game selesai
        for(int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        shotBird = Birds[0];
        SlingShooter.InitiateBird(Birds[0]);
    }


    public void ChangeBird()
    {
        TapCollider.enabled = false;

        //menghilangkan burung
        Birds.RemoveAt(0);

        if (isGameEnded)
        {
            return;
        }

        if(Birds.Count > 0)
        {
            shotBird = Birds[0];
            SlingShooter.InitiateBird(Birds[0]);
        }
            
    }


    //kondisi game selesai
    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                ShowPanel();
            }
        }

        //jika enemy udah abis maka kondisi game akan selesai
        if(Enemies.Count == 0)
        {
            isGameEnded = true;
        }
    }

    public void ShowPanel()
    {
        Panel.gameObject.SetActive(true);
    }

    public void TakeDamage(float Damage)
    {
        if (gameObject != null)
        {    
            // Do something  
            Destroy(gameObject);
        }
    }

    void OnMouseUp()
    {
        if(shotBird != null)
        {
            shotBird.OnTap();
        }
    }

}


