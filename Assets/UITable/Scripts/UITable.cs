using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class UITable : MonoBehaviour
    {
        [Header("Customization")]
        [SerializeField] private bool Striped = true;

        [SerializeField] private Color MainColor = new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color SecondColor = new Color(0.7529412f, 0.7529412f, 0.7529412f, 1f);

        [SerializeField] private GameObject RowPrefab;
        [SerializeField] private string InstanceName = "Row";
        [SerializeField] private GameObject HorizontalSeparatorPrefab;

        [Header("System")]
        [SerializeField] private Transform Container;

        public Action RowsGenerated;

        public int RowsCount
        {
            get {
                return Rows.Count;
            }
        }

        private List<GameObject> Rows = new List<GameObject>(20);

        public void RegenerateRows(int rowCount)
        {
            Clear();

            if (rowCount < 1) {
                return;
            }

            for (var i = 0; i < rowCount; i++) {
                var row = Instantiate(RowPrefab, Container);
                row.name = InstanceName + (i + 1).ToString();
                var background = row.GetComponent<Image>();
                if (!Striped) {
                    background.color = MainColor;
                }

                if (Striped) {
                    if (IsEvenNumber(i)) {
                        background.color = MainColor;
                    } else {
                        background.color = SecondColor;
                    }
                }
                Rows.Add(row);

                Instantiate(HorizontalSeparatorPrefab, Container);
            }

            RowsGenerated?.Invoke();
        }

        public GameObject GetRow(int index)
        {
            if (Rows == null || Rows.Count < 1) {
                return null;
            }

            if (index >= Rows.Count) {
                return null;
            }

            return Rows[index];
        }

        public GameObject GetCell(int rowIndex, int columnIndex)
        {
            var row = GetRow(rowIndex);
            if (row == null) {
                return null;
            }

            if (columnIndex * 2 >= row.transform.childCount) {
                return null;
            }

            return row.transform.GetChild(columnIndex * 2).gameObject;
        }

        public void Clear()
        {
            if(Container == null || Container.childCount < 1) {
                return;
            }

            if (Rows?.Count > 0) {
                Rows.Clear();
            }

            foreach (Transform child in Container) {
                Destroy(child.gameObject);
            }
        }

        public static bool IsEvenNumber(int x)
        {
            return x % 2 == 0;
        }
    }
}
