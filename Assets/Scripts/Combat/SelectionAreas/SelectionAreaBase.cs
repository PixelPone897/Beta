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
        protected Vector3Int OriginPoint { get; set; }
        protected HashSet<Vector3Int> selectionArea;
        public List<Vector3Int> selectionAreaList;

        public SelectionAreaBase()
        {
            OriginPoint = Vector3Int.zero;
            selectionArea = new HashSet<Vector3Int>();
            selectionAreaList = new List<Vector3Int>();
        }

        public bool ContainsPosition(Vector3Int position)
        {
            return selectionArea.Contains(position);
        }

        public abstract void UpdateSelectionArea(Vector3Int newOriginPoint);
        public abstract void ResetValues();
    }
}
