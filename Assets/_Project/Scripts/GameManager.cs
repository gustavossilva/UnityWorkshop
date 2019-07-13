using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Cria a referência estática para instância
    public static GameManager instance = null;

    public GameObject gameOverUi;
    public bool dontDestroyOnLoad = false;
    public bool hasStarted = false;
    public bool gameOver = false;
    
    //Lógicas do score;
    public TextMeshProUGUI scoreUI;
    public int score = 0;
    public double time = 0;
    public AudioSource scoreSound;

    void Awake() {
        Time.timeScale = 1;
        scoreSound = GetComponent<AudioSource>();
        //Se não há uma instância inicializada ainda para esse script, cria ela
        if (instance == null) {
            instance = this;
        }
        //Caso exista uma instância e você crie outra, destrói a nova instância (lembrar: singleton = somente uma instância)
        else if (instance != this) {
            Destroy(gameObject);
        }
        //Seta se a instância deve ser permanecida entre telas
        if (dontDestroyOnLoad) {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        if(gameOver) {
            gameOverUi.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        if (!hasStarted) {
            return;
        }


        //Faz as lógicas com o tempo passado no game e faz a pontuação
        time += Time.deltaTime;
        if (time > 0.1) {
            score++;
            time = 0;
            scoreUI.text = score.ToString();
        }
        if (score != 0 && score % 100 == 0) {
            scoreSound.Play();
        }
    }

    public void RestartGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
