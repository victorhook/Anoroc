using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static LevelHandler;

public class SkillButton : MonoBehaviour {

    public Text uiText;
    public LevelHandler levelHandler;
    public LevelHandler.Skills skill;
    private KeyCode levelUpKey;
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
     };

    void Start() {
        levelUpKey = keyCodes[(int) skill];
        SetHighlightColor();
        UpdateUI();
    }

    /* unity engine crashes every time I try to access
        the color-picker so I have to set the highlight-
        color of the buttons from script ...        */
    private void SetHighlightColor() {
        Button button = GetComponent<Button>();
        ColorBlock coloVar = button.colors;
        coloVar.highlightedColor = new Color(1f, .3f, .7f);
        button.colors = coloVar;
    }

    void Update() {
        if (Input.GetKeyDown(levelUpKey)) {
            Click();
        }
    }

    private void UpdateUI() {
        uiText.text = levelHandler.GetLevel(skill).ToString();
    }

    public void Click() {
        if (levelHandler.CanSkillLevelUp(skill)) {
            levelHandler.SkillLevelUp(skill);
            UpdateUI();
        }
    }

}
