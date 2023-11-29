using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTextComponent;
    [SerializeField] List<string> lines;
    [SerializeField] float textSpeed;

    public bool hasEnded = false;
    private int index;

    public static DialogManager Instance { get; private set; }

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    #endregion


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && lines.Count > 0 && !hasEnded)
        {
            if (dialogueTextComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                if (!hasEnded)
                {
                    StopAllCoroutines();
                    dialogueTextComponent.text = lines[index];
                }
                else
                {
                    StopAllCoroutines();
                }
            }
        }
    }

    public void startDialogLines()
    {
        index = 0;
        if (lines.Count > 0)
        {
            dialogueTextComponent.text = string.Empty;
            hasEnded = false;
            dialogueTextComponent.gameObject.SetActive(true);
            StartDialog();
        }
    }

    private void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueTextComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Count - 1 && !hasEnded)
        {
            index++;
            dialogueTextComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            //End Dialog
            dialogueTextComponent.gameObject.SetActive(false);
            dialogueTextComponent.text = string.Empty;
            hasEnded = true;
        }
    }

    public void SetLines(List<string> newLines)
    {
        lines = newLines;
    }
}
