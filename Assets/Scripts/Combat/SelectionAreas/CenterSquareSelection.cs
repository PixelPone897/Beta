using Assets.Scripts.Combat.SelectionAreas;
using System;
using System.Linq;
using UnityEngine;

namespace Scripts.CombatStates.SelectionAreas
{
    [Serializable]
    public class CenterSquareSelection : SelectionAreaBase
    {

        [SerializeField]
        private int halfWidth;

        public CenterSquareSelection() { }

        public override void UpdateSelectionArea(Vector3Int newOriginPoint)
        {
            OriginPoint = newOriginPoint;
            ResetValues();

            Vector3Int leftCorner = new Vector3Int(newOriginPoint.x - halfWidth, newOriginPoint.y - halfWidth);

            int width = (halfWidth * 2) + 1;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    selectionArea.Add(new Vector3Int(leftCorner.x + i, leftCorner.y + j));
                }
            }

            selectionAreaList = selectionArea.ToList();
        }

        public override void ResetValues()
        {
            selectionArea.Clear();
            selectionAreaList.Clear();
        }
    }
}