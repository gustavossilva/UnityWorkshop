using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Cria a referência estática para instância
    public static GameManager instance = null;
    public bool dontDestroyOnLoad = false;
    
    void Awake() {
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
}
