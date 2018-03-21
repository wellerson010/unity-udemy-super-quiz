using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temaInfo : MonoBehaviour {
    [Header("Configuração do tema")]
    public int idTema;
    public bool RequerNotaMinima;
    public float NotaMinimaNecessaria;

    [Header("Configuração do botão")]
    public Text txtTema;

    private temaScene temaScene;

    public string nomeDoTema;

    public Color corDoTema;

    [Header("Configuração das estrelas")]
    public int notaMinima1Estrelas;
    public int notaMinima2Estrelas;

    public GameObject[] estrela;

    private float notaFinal;

    private Button button;

    private soundController SoundController;

    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();

        temaScene = FindObjectOfType<temaScene>();

        txtTema.text = idTema.ToString();

        notaFinal = PlayerPrefs.GetFloat("notaFinal_" + idTema);
        print("notaFinal" + notaFinal + " - idtema" + idTema);
        estrelas();

        VerificarNotaMinima();
        SoundController = FindObjectOfType<soundController>();
    }

    void VerificarNotaMinima()
    {
        button.interactable = false;

        if (RequerNotaMinima)
        {
            float notaTemaAnterior = PlayerPrefs.GetFloat("notaFinal_" + (idTema - 1));

            if (notaTemaAnterior >= NotaMinimaNecessaria)
            {
                button.interactable = true;
            }
        }
        else
        {
            button.interactable = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void selecionarTema()
    {
        SoundController.PlayButtonSound();
        temaScene.nomeTemaTxt.text = nomeDoTema;
        temaScene.nomeTemaTxt.color = corDoTema;

        PlayerPrefs.SetInt("idTema", idTema);
        PlayerPrefs.SetString("nomeDoTema", nomeDoTema);
        PlayerPrefs.SetInt("notaMinima1Estrelas", notaMinima1Estrelas);
        PlayerPrefs.SetInt("notaMinima2Estrelas", notaMinima2Estrelas);

        temaScene.btnPlay.interactable = true;
    }

    public void estrelas()
    {
        foreach (GameObject e in estrela)
        {
            e.SetActive(false);
        }

        int totalEstrelas = (notaFinal == 10) ? 3 : (notaFinal >= notaMinima2Estrelas) ? 2 : (notaFinal >= notaMinima1Estrelas) ? 1 : 0;

        for (int i=0; i < totalEstrelas; i++)
        {
            estrela[i].SetActive(true);
        }
    }
}
