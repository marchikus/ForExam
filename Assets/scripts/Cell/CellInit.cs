using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInit : MonoBehaviour
{
    [SerializeField] private ClickableCell clickableCell;

    private void Start()
    {
        GameModel model = new GameModel();

        clickableCell.Initialize(model);
    }
}
