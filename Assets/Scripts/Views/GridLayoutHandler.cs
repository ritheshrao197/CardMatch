using MatchGame.Core;
using MatchGame.Data;
using UnityEngine;
using UnityEngine.UI;
namespace MatchGame.View
{
    public class GridLayoutHandler : View
    {
        public GridLayoutGroup gridLayout;
    
        public Vector2 cellSpacing = new Vector2(10f, 10f); // Adjust the spacing here

        GameDataContainer _gameDataContainer;
        private void Start()
        {
            Init();
        }
        public override void Init()
        {
            base.Init();
            gridLayout = gridLayout.GetComponent<GridLayoutGroup>();
            _gameDataContainer = ServiceLocator.Instance.Get<GameDataContainer>();
            eventHandlerSystem.AddListener(GameEventKeys.IndexNumbersUpdated, UpdateGridLayoutSize);
        }
        public override void Finalise()
        {
            base.Finalise();
            eventHandlerSystem.RemoveListener(GameEventKeys.IndexNumbersUpdated, UpdateGridLayoutSize);
        }
        public void UpdateGridLayoutSize()
        {
            // Ensure the grid layout exists.
            if (gridLayout == null)
            {
                Debug.LogError("Grid Layout is not assigned.");
                return;
            }
         

            // Calculate the cell size with spacing.
            //float cellWidth = gridLayout.GetComponent<RectTransform>().rect.width / 5;
            //float cellHeight = gridLayout.GetComponent<RectTransform>().rect.height  / 5;
            float cellWidth = (gridLayout.GetComponent<RectTransform>().rect.width - (_gameDataContainer.GridSize.columns - 1) * cellSpacing.x) / _gameDataContainer.GridSize.columns;
            float cellHeight = (gridLayout.GetComponent<RectTransform>().rect.height - (_gameDataContainer.GridSize.rows - 1) * cellSpacing.y) / _gameDataContainer.GridSize.rows;
           
            gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            gridLayout.spacing = cellSpacing; // Set the spacing.
        }

       
    }
}