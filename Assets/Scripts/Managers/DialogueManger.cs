using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System.Linq;

/// <summary>
/// Clase para controlar los dialogos con NPCs(Non Playable Characters)
/// </summary>
public class DialogueManger : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    public static DialogueManger instance { get; private set; }

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    /// <summary>
    /// Al iniciar seteamos los parámetros
    /// </summary>
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    /// <summary>
    /// Actualizamos según avance el dialogo para recoger y mostrar las opciones que tengamos
    /// </summary>
    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueDialogue();
        }
    }

    /// <summary>
    /// Iniciamos el dialogo
    /// </summary>
    /// <param name="inkJSON"></param>
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);

        //Mapear items para usar el dialogo(Solo se usarán los que existan en el dialogo
        foreach (string item in GameManager.instance.keys)
        {
            if (currentStory.variablesState[item] != null)
            {
                currentStory.variablesState[item] = GameManager.instance.values[GameManager.instance.keys.IndexOf(item)];
            }
        }

        //Mapear valores de si conoce al NPC(Solo se usarán los que existan en el diálogo
        foreach (string item in GameManager.instance.noConoceIndex)
        {
            if (currentStory.variablesState[item] != null)
            {
                currentStory.variablesState[item] = GameManager.instance.noConoceValues[GameManager.instance.noConoceIndex.IndexOf(item)];
            }
        }

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueDialogue();
    }

    /// <summary>
    /// Al salir del modo diálogo guardamos los valores actualizados según hayan cambiado durante el diálogo
    /// </summary>
    private void ExitDialogueMode()
    {
        foreach (string item in GameManager.instance.keys)
        {
            if (currentStory.variablesState[item] != null)
            {
                GameManager.instance.values[GameManager.instance.keys.IndexOf(item)] = (int)currentStory.variablesState[item];
            }
        }

        foreach (string item in GameManager.instance.noConoceIndex)
        {
            if (currentStory.variablesState[item] != null)
            {
                GameManager.instance.noConoceValues[GameManager.instance.noConoceIndex.IndexOf(item)] = (int)currentStory.variablesState[item];
            }
        }

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        Inventory.instance.Actualizar();
    }

    /// <summary>
    /// Continuar con la siguiente parte del diálogo
    /// </summary>
    private void ContinueDialogue()
    {
        if (currentStory.canContinue)
        {
            //Texto de linea actual
            dialogueText.text = currentStory.Continue();

            //Mostrar opciones, si las hay
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    /// <summary>
    /// Mostrar las opciones que puede elegir el jugador
    /// </summary>
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)//Para estar seguros de que podemos mostrar el número de opciones que tenemos
        {
            Debug.LogError("More choices were given tan the UI can andle. Number of choices: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    /// <summary>
    /// Seleccionar valor por defecto
    /// </summary>
    /// <returns></returns>
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    /// <summary>
    /// Elegir una opción
    /// </summary>
    /// <param name="choiceIndex"></param>
    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        InputManager.GetInstance().RegisterSubmitPressed();
        ContinueDialogue();
    }
}
