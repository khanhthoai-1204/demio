using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 
    [SerializeField] float moveSpeed = 10f;
    //
    [SerializeField] float padding = 1f;
    //
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    //
    [SerializeField] float projectileFiringPeriod = 0.1f;
    //
    Coroutine firingCoroutine;
    //
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    //------------------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //Framework
        SetUpMoveBoundaries();
        //print message
        StartCoroutine(PrintAndWait());
    }
    //------------------------------
    private void SetUpMoveBoundaries()
    {
        // khung
        //viewportToWorldPoint
        Camera gameCamera = Camera.main;
        // x
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        // y
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).y + padding;

    }
    //------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        //
        Move();
        // 
        Fire();
    }
    // print message
    IEnumerator PrintAndWait()
    {
        Debug.Log("First message sent, boss");
            yield return new WaitForSeconds(3);
        Debug.Log("The second message !!!");
    }
    //------------------------------
    private void Move()
    {
        // link edit -> project -> input manager -> horizontal
        // x
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        // y
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        //---
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
        // debug console trong unity
        Debug.Log(deltaX);

    }
    //------------------------------
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))// nhan
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))// nha
        {
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator FireContinously()
    {
        while (true)// ban lien tuc
        {
            // add Rigidbody2D in PlayerLaser ---- angular drag = 0 --- gravity scale = 0 thang 1 roi
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);

        }
    }






}
