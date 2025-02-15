using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject missilePrefab;
    public Transform missilePosition;
    public GameObject effectDestroy;
    public GameObject gameOver;
    public GameObject UIObject;
    public GameObject nextScene;
    public GameObject gameManage;

    public AudioSource audioSource;
    public AudioClip stoneHitSound;
    public AudioClip starCollectSound;
    public AudioClip shotSound;

    // Start is called before the first frame update
    void Start()
    {
        speed = 15f;

        // T? ð?ng l?y AudioSource n?u chýa có
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // Thêm n?u chýa có
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerShoot();
    }

    void PlayerMove()
    {
        float xPosition = Input.GetAxis("Horizontal");
        float yPosition = Input.GetAxis("Vertical");
        Vector3 v = new Vector3(xPosition, yPosition, 0) * speed * Time.deltaTime;
        transform.Translate(v);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(shotSound);
            GameObject gm = Instantiate(missilePrefab, missilePosition);
            gm.transform.SetParent(null);
            Destroy(gm, 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            audioSource.PlayOneShot(stoneHitSound); // Phát âm thanh va ch?m ðá
            GameObject gm = Instantiate(effectDestroy, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gm, 1f);
            StartCoroutine(WaitAndShowGameOver()); // G?i Coroutine
        }

        if (collision.CompareTag("Star"))
        {
            audioSource.PlayOneShot(starCollectSound); // Phát âm thanh khi thu th?p sao
            ShowNextScene();
            Destroy(collision.gameObject);
            UIObject.GetComponent<UIController>().InscreaseScore();
        }

        if (collision.CompareTag("NextScene"))
        {
            NextScene();
        }
    }

    public void ShowNextScene()
    {
        int score = UIObject.GetComponent<UIController>().GetScore();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int scoreToPass = 5;
        if (currentIndex >= 3)
        {
            scoreToPass = 10;
        }

        if (score >= scoreToPass)
        {
            Vector3 v = new Vector3(0f, 3f, 0f);
            Instantiate(nextScene, v, Quaternion.identity);
            gameManage.GetComponent<GameManage>().CancelInvoke();
        }
    }

    public void NextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
    IEnumerator WaitAndShowGameOver()
    {
        yield return new WaitForSeconds(0.5f); // Ch? 1 giây
        gameOver.SetActive(true); // Hi?n th? Game Over
        Destroy(this.gameObject); // Xóa nhân v?t
    }

}
