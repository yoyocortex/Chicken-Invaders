using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour {
    public Invader[] prefabs;
    public int rows = 3;
    public int columns = 6;
    public AnimationCurve speed;
    public Projectile missilePrefab;
    private float _missileAttackRate = 1.0f;
    private Vector3 _direction = Vector2.right;
    public int invadersKilled { get; private set; }
    public int invadersAlive => totalInvaders - invadersKilled;
    public int totalInvaders => rows * columns;
    public float precentKilled => (float)invadersKilled / (float)totalInvaders;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            for (int row = 0; row < rows; row++)
            {
                float width = 2.0f * (columns - 1);
                float height = 2.0f * (rows - 1);
                Vector2 centering = new Vector2(-width / 2, -height / 2);
                Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);
                for (int column = 0; column < columns; column++)
                {
                    var invader = Instantiate(prefabs[row], transform);
                    invader.killed += InvaderKilled;
                    Vector3 position = rowPosition;
                    position.x += column * 2.0f;
                    invader.transform.localPosition = position;
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            for (int row = 0; row < rows; row++)
            {
                float width = 1.0f * (columns - 1);
                float height = 1.0f * (rows - 1);
                Vector2 centering = new Vector2(-width / 2, -height / 2);
                Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.0f), 0.0f);
                for (int column = 0; column < columns; column++)
                {
                    var invader = Instantiate(prefabs[row], transform);
                    invader.killed += InvaderKilled;
                    Vector3 position = rowPosition;
                    position.x += column * 1.0f;
                    invader.transform.localPosition = position;
                }
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            for (int row = 0; row < rows; row++)
            {
                float width = 1.2f * (columns - 1);
                float height = 1.0f * (rows - 1);
                Vector2 centering = new Vector2(-width / 2, -height / 2);
                Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.0f), 0.0f);
                for (int column = 0; column < columns; column++)
                {
                    var invader = Instantiate(prefabs[row], transform);
                    invader.killed += InvaderKilled;
                    Vector3 position = rowPosition;
                    position.x += column * 1.2f;
                    invader.transform.localPosition = position;
                }
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), _missileAttackRate, _missileAttackRate);
    }
    private void Update()
    {
        transform.position += _direction * (speed.Evaluate(precentKilled) * Time.deltaTime);

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(_direction.Equals(Vector3.right) && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }
            if(_direction.Equals(Vector3.left) && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()
    {
        _direction.x *= -1.0f;
        Vector3 position = transform.position;
        position.y -= 0.3f;
        transform.position = position;
    }
    private void MissileAttack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(Random.value < (1.0f / (float)invadersAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            } 
        }
    }
    private void InvaderKilled()
    {
        invadersKilled++;

        if (invadersAlive == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
