using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnCommands : MonoBehaviour {
    private soundController SoundController;

    void Start()
    {
        SoundController = FindObjectOfType<soundController>();
    }

    public void irCena(string nomeCena)
    {
        string sceneNameActual = SceneManager.GetActiveScene().name;

        if (sceneNameActual != "titulo" && sceneNameActual != "temas")
        {
            SoundController.AudioSourceMusic.clip = SoundController.Musics[0];
            SoundController.AudioSourceMusic.Play();
        }

        SoundController.PlayButtonSound();
        SceneManager.LoadScene(nomeCena);
    }




    //mobile e pc
    public void sair()
    {
        Application.Quit();
    }

    public void jogarNovamente()
    {
        SoundController.PlayButtonSound();
        int idCena = PlayerPrefs.GetInt("idTema");

        if (idCena != 0)
        {
            SceneManager.LoadScene(idCena.ToString());
        }
    }


}
