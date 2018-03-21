using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class temaScene : MonoBehaviour {
    public Text nomeTemaTxt;
    public Button btnPlay;

    [Header("Configuração da paginação")]
    public Button ButtonPreviousPage;
    public Button ButtonNextPage;
    public GameObject[] PanelThemes;

    private int PageActual;
    private soundController SoundController;

    // Use this for initialization
    void Start () {
        btnPlay.interactable = false;

        foreach(GameObject panel in PanelThemes)
        {
            panel.SetActive(false);
        }

        PanelThemes[0].SetActive(true);

        ButtonPreviousPage.gameObject.SetActive(PanelThemes.Length > 1);
        ButtonNextPage.gameObject.SetActive(PanelThemes.Length > 1);

        SoundController = FindObjectOfType<soundController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void jogar()
    {
        SoundController.PlayButtonSound();
        int idCena = PlayerPrefs.GetInt("idTema");

        if (idCena != 0)
        {
            SoundController.AudioSourceMusic.clip = SoundController.Musics[1];
            SoundController.AudioSourceMusic.Play();
            SceneManager.LoadScene(idCena.ToString());
        }
    }

    public void GoToPage(int i)
    {
        SoundController = FindObjectOfType<soundController>();

        PageActual += i;
        print(PageActual);

        btnPlay.interactable = false;
        nomeTemaTxt.text = "Selecione um tema";
        nomeTemaTxt.color = Color.white;

        if(PageActual < 0)
        {
            PageActual = PanelThemes.Length - 1;
            print(PageActual);
        }
        if (PageActual >= PanelThemes.Length)
        {
            PageActual = 0;
            print(PageActual);
        }

        foreach (GameObject panel in PanelThemes)
        {
            panel.SetActive(false);
        }

        PanelThemes[PageActual].SetActive(true);
    }
}
