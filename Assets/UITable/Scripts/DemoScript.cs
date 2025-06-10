using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.UI;

public class DemoScript : MonoBehaviour
{
    [SerializeField] private UITable Table;
    [SerializeField] private Button ButtonClear;

    [Header("DemoData")]
    [SerializeField] private string[] Date;
    [SerializeField] private string[] Day;
    [SerializeField] private string[] Night;
    [SerializeField] private string[] Total;

    void Awake()
    {
        Table.RowsGenerated += OnRowsGenerated;
    }

    void Start()
    {
        ButtonClear.onClick.AddListener(OnClickButtonClear);
    }

    void OnEnable()
    {
        Table.RegenerateRows(Date.Length);
    }

    void OnDisable()
    {
        Table.Clear();
    }

    private void OnRowsGenerated()
    {
        for (var i = 0; i < Table.RowsCount; i++) {
            Table.GetCell(i, 0).GetComponent<TMP_Text>().text = Date[i].Substring(0, 5) + "\n" + Date[i].Substring(6, 4);
            Table.GetCell(i, 1).GetComponent<TMP_Text>().text = Day[i];
            Table.GetCell(i, 2).GetComponent<TMP_Text>().text = Night[i];
            Table.GetCell(i, 3).GetComponent<TMP_Text>().text = Total[i];
        }
    }

    private void OnClickButtonClear()
    {
        Table.Clear();
    }

    void OnDestroy()
    {
        Table.RowsGenerated -= OnRowsGenerated;
        ButtonClear.onClick.RemoveListener(OnClickButtonClear);
    }
}
