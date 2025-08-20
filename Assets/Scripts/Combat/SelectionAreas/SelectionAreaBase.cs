using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Combat.SelectionAreas
{
    [Serializable]
    public abstract class SelectionAreaBase
    {
        protected Vector2Int OriginPoint { get; set; }
        protected HashSet<Vector2Int> selectionArea;
        protected List<Vector2Int> selectionAreaList;

        public SelectionAreaBase()
        {
            OriginPoint = Vector2Int.zero;
            selectionArea = new HashSet<Vector2Int>();
            selectionAreaList = new List<Vector2Int>();
        }

        public bool ContainsPosition(Vector2Int position)
        {
            return selectionArea.Contains(position);
        }

        public abstract void UpdateSelectionArea(Vector2Int newOriginPoint);
        public abstract void ResetValues();
    }
}
