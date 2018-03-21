using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class modoJogo1 : MonoBehaviour
{

    [Header("Configuração dos textos")]
    public Text nomeTemaTxt;

    public Text UIQuestionText;

    public Image UIQuestionImage;

    public Text infoRespostasTxt;

    public Text notaFinalTxt;

    public Text msg1Txt;

    public Text msg2Txt;

    [Header("Configuração das alternativas")]
    public Text UIAlternativeAText;

    public Text UIAlternativeBText;

    public Text UIAlternativeCText;

    public Text UIAlternativeDText;

    [Header("Configuração das imagens")]
    public Image UIAlternativeAImage;

    public Image UIAlternativeBImage;

    public Image UIAlternativeCImage;

    public Image UIAlternativeDImage;

    [Header("Configuração das barras")]
    public GameObject barraProgresso;

    public GameObject barraTempo;

    [Header("Configuração dos botões")]
    public Button[] AnswerButtons;
    public Color ColorRight, ColorWrong;

    [Header("Configuração do modo de jogo")]
    public bool IsRandom;
    public Sprite[] QuestionImages;
    public bool jogarComTempo;
    public float tempoResponder;
    public bool IsMultiplesChoice;
    public bool IsMultiplesChoiceWithImage;
    public bool QuestionWithImages;

    [Header("Configuração de cores")]
    public Color corZero;
    public Color corUm;
    public Color corDois;
    public Color corTres;

    [Header("Configuração das perguntas")]
    public string[] QuestionsText;

    public string[] Answers;

    public int TotalQuestionsInSession;

    public bool IsToShowCorrectAnimation;

    public int quantidadePiscar;

    private int QuestionId, QuestionsAnswered, TotalCorrects, notaMinima1Estrelas, notaMinima2Estrelas, idTema, ButtonCorrectId;

    private bool ShowingCorrectAnimation;

    private float percProgresso, notaFinal, valorQuestao, percTempo, TemporaryTime;

    private List<int> ListFinalQuestions;

    [Header("Configuração das alternativas")]
    public string[] AnswersA;
    public string[] AnswersB;
    public string[] AnswersC;
    public string[] AnswersD;

    [Header("Configuração das images das alternativas")]
    public Sprite[] AnswersImageA;
    public Sprite[] AnswersImageB;
    public Sprite[] AnswersImageC;
    public Sprite[] AnswersImageD;


    [Header("Configuração dos paineis")]
    public GameObject painelResponder;
    public GameObject painelResultado;
    public GameObject[] estrelas;

    [Header("Configuração das mensagens")]
    public string[] tituloMensagens;
    public string[] subtituloMensagem;

    private soundController SoundController;

    // Use this for initialization
    void Start()
    {
        SoundController = FindObjectOfType<soundController>();
        idTema = PlayerPrefs.GetInt("idTema");
        notaMinima1Estrelas = PlayerPrefs.GetInt("notaMinima1Estrelas");
        notaMinima2Estrelas = PlayerPrefs.GetInt("notaMinima2Estrelas");

        nomeTemaTxt.text = PlayerPrefs.GetString("nomeDoTema");

        ListFinalQuestions = BuildListQuestions();
        BuildUIQuestion();

        MakeProgressionBar();

        valorQuestao = 10 / (float)ListFinalQuestions.Count();
        controleBarraTempo();

        painelResponder.SetActive(true);
        painelResultado.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (jogarComTempo && !ShowingCorrectAnimation)
        {
            TemporaryTime += Time.deltaTime;
            controleBarraTempo();

            if (TemporaryTime >= tempoResponder)
            {
                NextQuestion();
            }
        }
    }

    private int GetTotalOriginalQuestion()
    {
        return (QuestionWithImages) ? QuestionImages.Length : QuestionsText.Length;
    }

    private List<int> RandomyzeQuestions()
    {
        List<int> listFinalQuestions = new List<int>();

        if (TotalQuestionsInSession > GetTotalOriginalQuestion())
        {
            TotalQuestionsInSession = GetTotalOriginalQuestion();
        }

        while (ListFinalQuestions.Count < TotalQuestionsInSession)
        {
            int random = Random.Range(0, GetTotalOriginalQuestion());

            if (ListFinalQuestions.Where(x => x == random).Count() > 0)
            {
                continue;
            }

            listFinalQuestions.Add(random);
        }

        return listFinalQuestions;
    }

    private List<int> SequentialQuestions()
    {
        List<int> listFinalQuestions = new List<int>();

        if (TotalQuestionsInSession > GetTotalOriginalQuestion())
        {
            TotalQuestionsInSession = GetTotalOriginalQuestion();
        }

        for (int i = 0; i < TotalQuestionsInSession; i++)
        {
            listFinalQuestions.Add(i);
        }

        return listFinalQuestions;
    }

    private List<int> BuildListQuestions()
    {
        List<int> listFinalQuestions = new List<int>();

        if (IsRandom)
        {
            listFinalQuestions = RandomyzeQuestions();
        }
        else
        {
            listFinalQuestions = SequentialQuestions();
        }

        return listFinalQuestions;
    }

    private void BuildUIQuestion()
    {
        if (QuestionWithImages)
        {
            UIQuestionImage.sprite = QuestionImages[ListFinalQuestions[QuestionId]];
        }
        else
        {
            UIQuestionText.text = QuestionsText[ListFinalQuestions[QuestionId]];
        }

        if (IsMultiplesChoice)
        {
            UIAlternativeAText.text = AnswersA[ListFinalQuestions[QuestionId]];
            UIAlternativeBText.text = AnswersB[ListFinalQuestions[QuestionId]];
            UIAlternativeCText.text = AnswersC[ListFinalQuestions[QuestionId]];
            UIAlternativeDText.text = AnswersD[ListFinalQuestions[QuestionId]];
        }
        else if (IsMultiplesChoiceWithImage)
        {
            UIAlternativeAImage.sprite = AnswersImageA[ListFinalQuestions[QuestionId]];
            UIAlternativeBImage.sprite = AnswersImageB[ListFinalQuestions[QuestionId]];
            UIAlternativeCImage.sprite = AnswersImageC[ListFinalQuestions[QuestionId]];
            UIAlternativeDImage.sprite = AnswersImageD[ListFinalQuestions[QuestionId]];
        }
    }

    private void SetButtonCorrectId()
    {
        switch (Answers[ListFinalQuestions[QuestionId]])
        {
            case "A":
                ButtonCorrectId = 0;
                break;
            case "B":
                ButtonCorrectId = 1;
                break;
            case "C":
                ButtonCorrectId = 2;
                break;
            case "D":
                ButtonCorrectId = 3;
                break;
        }
    }

    public void Answer(string answer)
    {
        if (ShowingCorrectAnimation)
        {
            return;
        }

        if (Answers[ListFinalQuestions[QuestionId]] == answer)
        {
            SoundController.PlayRightSound();
            TotalCorrects++;
        }
        else
        {
            SoundController.PlayWrongSound();
        }

       SetButtonCorrectId();

        if (IsToShowCorrectAnimation)
        {
            PrepareCorrectAnimation();
        }
        else
        {
            StartCoroutine("WaitForNewQuestion");
        }
    }

    private void PrepareCorrectAnimation()
    {
        foreach (Button button in AnswerButtons)
        {
            button.GetComponent<Image>().color = ColorWrong;
        }
        ShowingCorrectAnimation = true;
        AnswerButtons[ButtonCorrectId].GetComponent<Image>().color = ColorRight;

        StartCoroutine("ShowCorrectAnimation");
    }

    IEnumerator WaitForNewQuestion()
    {
        ShowingCorrectAnimation = true;
        yield return new WaitForSeconds(1f);
        ShowingCorrectAnimation = false;
        NextQuestion();
    }

    IEnumerator ShowCorrectAnimation()
    {
        for (int i = 0; i < quantidadePiscar; i++)
        {
            AnswerButtons[ButtonCorrectId].image.color = ColorRight;
            yield return new WaitForSeconds(0.2f);
            AnswerButtons[ButtonCorrectId].image.color = Color.white;

            yield return new WaitForSeconds(0.2f);
        }

        foreach (Button button in AnswerButtons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        ShowingCorrectAnimation = false;

        NextQuestion();
    }

    public void NextQuestion()
    {
        QuestionId++;
        TemporaryTime = 0;

        EventSystem.current.SetSelectedGameObject(null);

        QuestionsAnswered++;
        MakeProgressionBar();

        if (QuestionId < ListFinalQuestions.Count())
        {
            BuildUIQuestion();
        }
        else
        {
            CalculateValueFinal();
        }
    }




    void MakeProgressionBar()
    {
    //    int totalPerguntas = (perguntasComImagem) ? perguntasImagens.Count() : perguntas.Length;

        percProgresso = ((float)QuestionsAnswered) / ListFinalQuestions.Count;
        infoRespostasTxt.text = string.Format("Respondeu pergunta {0} de {1} perguntas", QuestionsAnswered, ListFinalQuestions.Count);
        barraProgresso.transform.localScale = new Vector3(percProgresso, 1, 1);
    }

    void controleBarraTempo()
    {
        if (jogarComTempo)
        {
            barraTempo.SetActive(true);
        }
        else
        {
            barraTempo.SetActive(false);
        }

        percTempo = ((TemporaryTime - tempoResponder) / tempoResponder) * -1;

        if (percTempo < 0)
        {
            percTempo = 0;
        }

        barraTempo.transform.localScale = new Vector3(percTempo, 1, 1);
    }

    void CalculateValueFinal()
    {
        notaFinal = valorQuestao * TotalCorrects;
        painelResponder.SetActive(false);
        painelResultado.SetActive(true);
        notaFinalTxt.text = notaFinal.ToString();

        if (notaFinal > PlayerPrefs.GetFloat("notaFinal_" + idTema))
        {
            PlayerPrefs.SetFloat("notaFinal_" + idTema, notaFinal);
        }

        int totalEstrelas = (notaFinal == 10) ? 3 : (notaFinal >= notaMinima2Estrelas) ? 2 : (notaFinal >= notaMinima1Estrelas) ? 1 : 0;

        switch (totalEstrelas)
        {
            case 0:
                msg1Txt.color = corZero;
                break;
            case 1:
                msg1Txt.color = corUm;
                break;
            case 2:
                msg1Txt.color = corDois;
                break;
            case 3:
                msg1Txt.color = corTres;
            //    SoundController.Play3StarsSound();
                break;
        }

        foreach (GameObject e in estrelas)
        {
            e.SetActive(false);
        }

        for (int i = 0; i < totalEstrelas; i++)
        {
            estrelas[i].SetActive(true);
        }

        msg1Txt.text = tituloMensagens[totalEstrelas];
        msg2Txt.text = subtituloMensagem[totalEstrelas];
    }
}
