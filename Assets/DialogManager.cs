using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueTextComponent;
    [SerializeField] List<string> lines;
    [SerializeField] float textSpeed;

    private bool hasEnded = false;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTextComponent.text = string.Empty;
        StartDialog();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        dialogueTextComponent.text = string.Empty;
        StartDialog();
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
            hasEnded = true;
            dialogueTextComponent.text = string.Empty;
        }
    }
}
