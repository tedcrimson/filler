using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    private bool canPlay = true;
    private ShootController current;

    private ShootController bulletPrefab;


    // Use this for initialization
    void Start()
    {
        ShootController.OnHit += Pick;
        LevelManager.OnGameOver += ChangePlayable;
        LevelManager.OnChangeLevel += ChangePlayable;
        Pick(null);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        ShootController.OnHit -= Pick;
        LevelManager.OnGameOver -= ChangePlayable;
        LevelManager.OnChangeLevel -= ChangePlayable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canPlay)
        {
            Debug.Log(Input.GetMouseButtonDown(0));
            current.Throw();
        }
    }

    void Pick(ShootController s)
    {
        if(canPlay){
        current = Instantiate(bulletPrefab, this.transform);
		current.GetComponent<ShootController>().SetColor();
        }
        // bullets.RemoveAt(0);
    }

    public void SetHitObject(ShootController hito, int count)
    {
        bulletPrefab = hito;
    }

    public void ChangePlayable(int s)
    {
        canPlay = false;
    }


}
