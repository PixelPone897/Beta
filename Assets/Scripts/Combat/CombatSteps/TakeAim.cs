using Assets.Scripts.Combat.CombatActions;
using Assets.Scripts.Combat.SelectionAreas;
using Assets.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static SelectionAreaRenderer;

namespace Assets.Scripts.Combat.CombatSteps
{
    public class TakeAim : CombatStep
    {
        // This is temporary
        private Vector3Int centerPosition;
        private Vector3Int hoverPosition;

        private IInputService inputService;
        private ILoggerService loggerService;
        
        private BattleManager battleManager;
        private GameObject selectionPrefab;
        private GameObject hoverHandPrefab;
        private GameObject selectionInstance;
        private GameObject hoverInstance;

        private SelectionAreaRenderer areaRenderer;
        private SelectionAreaBase selectionBounds;

        public TakeAim(CombatAction parent,
            SelectionAreaBase selectionBounds,
            GameObject selectionPrefab,
            GameObject hoverHandPrefab) : base(parent)
        {
            this.selectionBounds = selectionBounds;

            battleManager = parent.ServiceProvider.GetContext<BattleManager>();
            inputService = parent.ServiceProvider.GetService<IInputService>();
            loggerService = parent.ServiceProvider.GetService<ILoggerService>();
            this.selectionPrefab = selectionPrefab;
            this.hoverHandPrefab = hoverHandPrefab;
        }

        public override void StartStep()
        {
            loggerService?.EnableLogging();
            loggerService?.Log("Enabled Take Aim Step!");
            inputService.EnableInput();
            inputService.OnMovePerformed += InputService_OnMovePerformed;

            selectionInstance = UnityEngine.Object.Instantiate(selectionPrefab);
            selectionInstance.transform.SetParent(battleManager.GridProperty.transform);
            areaRenderer = selectionInstance.GetComponent<SelectionAreaRenderer>();

            hoverInstance = UnityEngine.Object.Instantiate(hoverHandPrefab);
            hoverInstance.transform.SetParent(selectionInstance.transform);

            selectionBounds.UpdateSelectionArea(centerPosition);
            areaRenderer.Render(selectionBounds.selectionAreaList);
            SetHoverHandPosition();
        }

        private void InputService_OnMovePerformed(object sender, Vector2 input)
        {
            if (input == Vector2Int.left || input == Vector2Int.right
                || input == Vector2Int.up || input == Vector2Int.down)
            {
                Vector3 convertedInput = new Vector3(input.x, input.y);

                Vector3Int potentialNewPosition = hoverPosition + Vector3Int.RoundToInt(convertedInput);

                if (battleManager.MainGridMap.HasTile(potentialNewPosition)
                    && selectionBounds.ContainsPosition(potentialNewPosition))
                {
                    hoverPosition = potentialNewPosition;
                }

                loggerService.Log($"Updated Hover Position: {hoverPosition}");
                areaRenderer.Render(selectionBounds.selectionAreaList);
                SetHoverHandPosition();
            }
        }

        public void SetHoverHandPosition()
        {
            Vector3 position = areaRenderer.GetCellCenterWorldPosition(hoverPosition);

            position = new Vector3(position.x, position.y + 0.25f);
            hoverInstance.transform.position = position;
        }

        public override bool IsFinished()
        {
            return false;
        }

        public override void UpdateStep()
        {
            
        }
    }
}
