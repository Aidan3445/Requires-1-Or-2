using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CellType
{
    None,
    One,
    Two,
    Three,
    Four,
    Five,
    Six
}

public class CellBehavior : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public GridBehavior gridManager;
    public Color originalColor;
    public TextMeshProUGUI cellText;
    public AudioClip cellCorrect;
    public AudioClip cellIncorrect;
    public AudioClip cellNeutral;

    CellType cellType;
    int cellIndex;
    MeshRenderer _renderer;
    Color successColor = Color.green;
    Color failColor = Color.red;
    Color selectedColor = Color.white;
    AudioSource audioSource;
    

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.color = originalColor;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the cell is hit with the axe tip
        if (collision.collider.CompareTag("AxeTip"))
        {
            // Make the axe kinematic to freeze it
            collision.collider.GetComponentInParent<Rigidbody>().isKinematic = true;
            // Give the grid manager the index of this cell
            gridManager.OnCellHitListener(cellIndex);

        }
        Debug(collision.collider.tag);
    }

    private void Debug(string message)
    {
        if (debugText)
        {
            debugText.text = message;
        }
    }

    public void SetCellType(CellType type)
    {
        cellType = type;
        SetCellText(type);
    }

    void SetCellText(CellType type)
    {
        switch (type)
        {
            case CellType.One:
                cellText.text = "1";
                break;
            case CellType.Two:
                cellText.text = "2";
                break;
            case CellType.Three:
                cellText.text = "3";
                break;
            case CellType.Four:
                cellText.text = "4";
                break;
            case CellType.Five:
                cellText.text = "5";
                break;
            case CellType.Six:
                cellText.text = "6";
                break;
        }
    }

    public void ShowCellText()
    {
        cellText.enabled = true;
    }

    public void HideCellText()
    {
        cellText.enabled = false;
    }

    public void SetCellIndex(int index)
    {
        cellIndex = index;
    }

    public void Success()
    {
        audioSource.PlayOneShot(cellCorrect);
        _renderer.material.color = successColor;
        ShowCellText();
    }

    public void Failed()
    {
        audioSource.PlayOneShot(cellIncorrect);
        _renderer.material.color = failColor;
        ShowCellText();
        Invoke("Neutral", 3);
    }

    public void Neutral()
    {
        _renderer.material.color = originalColor;
        HideCellText();
    }

    public void Selected()
    {
        audioSource.PlayOneShot(cellNeutral);
        _renderer.material.color = selectedColor;
        ShowCellText();
    }

    public static string CellToString(CellType type)
    {
        switch (type)
        {
            case CellType.One:
                return "1";
            case CellType.Two:
                return "2";
            case CellType.Three:
                return "3";
            case CellType.Four:
                return "4";
            case CellType.Five:
                return "5";
            case CellType.Six:
                return "6";
        }
        return "";
    }
}
