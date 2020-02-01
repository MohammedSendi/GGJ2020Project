﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Linq;
using TMPro;

public class Loader : MonoBehaviour
{
    //this is what forms a sentence
    public bool autoStart;
    public bool Answer;
    public GameObject correctDialog;
    public GameObject wrongDialog;
    public int correctAnswer;

    public string Choise1;
    public string Choise2;

    private GameObject temp;

    public GameObject choices;

    public class sentence {
        public string background;
        public string name;
        public string dialog;
        public string expression;


        public sentence(string background, string name, string expression, string dialog) {
            this.background = background;
            this.name = name;
            this.dialog = dialog;
            this.expression = expression;
        }
    }

    //document
    public string documentName;
    private List<sentence> finalText;
    private List<sentence> sentences;

    //UI Elements
    private TextMeshProUGUI sName;
    private TextMeshProUGUI sDialogue;
    private Image characterExpression;
    private Image backgroundImage;
    private GameObject panles;
    private Canvas UI;

    //logic variables
    private int counter;
    private bool dialogueStarted = false;
    private bool isTyping = false;
    private bool skipTyping = false;

    void Start()
    {
        //find ui elements
        UI = GameObject.Find("_DialogueUI_UI").GetComponent<Canvas>();
        sName = GameObject.Find("_DialogueUI_Name").GetComponent<TextMeshProUGUI>();
        sDialogue = GameObject.Find("_DialogueUI_Dialogue").GetComponent<TextMeshProUGUI>();
        characterExpression = GameObject.Find("_DialogueUI_characterImage").GetComponent<Image>();
        backgroundImage = GameObject.Find("_DialogueUI_backgroundImage").GetComponent<Image>();
        panles = GameObject.Find("_DialogueUI_Panels") ;
        
    }

    void Update()
    {
        //this is just for testing
        if (dialogueStarted && Input.GetKeyDown(KeyCode.Mouse0)) {
            nextLine();
        }
        if (!dialogueStarted && (Input.GetKeyDown(KeyCode.Space) || autoStart)) {
            startDialog();
        }
    }
    
    //converts xmlfiles
    private List<sentence> parseFile() {

        TextAsset xmlAssset = Resources.Load<TextAsset>("DialogueSystem/Dialogues/" + documentName);
        var doc = XDocument.Parse(xmlAssset.text);

        var allDict = doc.Element("Document").Elements("page");
        List<sentence> allTextDic = new List<sentence>();



        foreach (var oneDict in allDict) {

            XElement Xbackground = oneDict.Element("background");
            XElement Xname = oneDict.Element("name");
            XElement Xexpression = oneDict.Element("expression");
            XElement Xdialogue = oneDict.Element("dialogue");

            string _background = Xbackground.Value;
            string _name = Xname.Value;
            string _expression = Xexpression.Value;
            string _dialogue = Xdialogue.Value;

            sentence sen = new sentence(_background ,_name, _expression, _dialogue);
            allTextDic.Add(sen);
        }
        return allTextDic;

    }
    //call this to start the dialogue
    public void startDialog() {

        //open UI
        UI.enabled = true;
        dialogueStarted = true;
        counter = 0;
        finalText = parseFile();
        sentence sentence = finalText[counter];
        getSentence(sentence);

    }
    //call this to go to the next paragraph in the dialogue
    public void nextLine() {
        if (isTyping)
        {
            skipTyping = true;
        }
        else
        {
            counter++;
            if (counter < finalText.Count)
            {
                sentence sentence = finalText[counter];
                getSentence(sentence);
            }
            else
            {
                

                if (correctAnswer == 0){
                    //disable UI
                    dialogueStarted = false;
                    UI.enabled = false;
                    autoStart = false;
                    //Destroy(this.gameObject);

                }
                else{
                    //show choices
                    //choices.SetActive(true);
                    temp = GameObject.Instantiate(choices, UI.gameObject.transform) as GameObject;
                    Destroy(temp.transform.GetChild(0));
                    temp.transform.GetChild(0).GetComponent<Button>().onClick.AddListener( ()=> this.checkAnswer(1) );
                    temp.transform.GetChild(1).GetComponent<Button>().onClick.AddListener( ()=> this.checkAnswer(2) );

                    foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
                    {
                        if(go.name == "BTN1")
                        {
                            go.GetComponentInChildren<Text>().text = Choise1;
                        }
                    }
                    foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject))){
                        if(go.name == "BTN2")
                        {
                            go.GetComponentInChildren<Text>().text = Choise2;
                        }
                    }
                    //change button text

                }
                                


            }
        }
    }
    void test(){
        Destroy(temp.transform.GetChild(1));
    }


    public void checkAnswer(int x)
    {
        Destroy(temp.gameObject);
        if (x == correctAnswer)
        {
            GameObject.Instantiate(correctDialog);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<btnFunction>().points++;
        }
        else
        {
            GameObject.Instantiate(wrongDialog);
            GameObject.FindGameObjectWithTag("Manager").GetComponent<btnFunction>().points--;

        }
        //choices.SetActive(false);
        UI.enabled = false; 
        Destroy(this.gameObject);
    }










    //get the character name and expression from scriptable object
    private void setCharacterAndExpression(string name, string expression) {

        Expressions character = Resources.Load<Expressions>("DialogueSystem/Characters/" + name);
        characterExpression.sprite = character.expressions[expression];
        sName.text = character.characterName;
    }
    //puts the sentence in the UI
    private void getSentence(sentence sentence) {
        //getting character
        if (sentence.name == "none")
        {
            panles.SetActive(false);
        }
        else
        {
            panles.SetActive(true);
            setCharacterAndExpression(sentence.name, sentence.expression);
            //sDialogue.text = sentence.dialog;
            StartCoroutine( typingDialogue(sentence.dialog, .2f) );
        }

        //getting background
        if (sentence.background == "none")
        {
            backgroundImage.enabled = false;
        }
        else
        {
            backgroundImage.enabled = true;
            backgroundImage.sprite = Resources.Load<Sprite>("DialogueSystem/Backgrounds/" + sentence.background);
        }
    }
    //Make typing effect
    private IEnumerator typingDialogue(string dialogue, float speed) {
        isTyping = true;
        sDialogue.text = "";
        for (int i = 0; i < dialogue.Length; i++) {
            if (skipTyping)
            {
                sDialogue.text = dialogue;
                skipTyping = false;
                break;
            }
            else
            {
                sDialogue.text += dialogue[i];
                yield return new WaitForSeconds(speed);
            }
        }
        isTyping = false;
        
    }



}
    