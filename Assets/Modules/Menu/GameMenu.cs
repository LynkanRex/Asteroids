using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Menu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuContainer;
        
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button quitButton;
        
        public Action<MenuActionType> MenuButtonClicked;

        public enum MenuActionType
        {
            NewGame,
            Quit
        }

        public void Start()
        {
            ShowMenu(true);
        }
        
        public void ShowMenu(bool shouldShow)
        {
            menuContainer.SetActive(shouldShow);
            
            newGameButton.onClick.AddListener(() => MenuButtonOnClicked(MenuActionType.NewGame));
            quitButton.onClick.AddListener(() => MenuButtonOnClicked(MenuActionType.Quit));
        }
        
        private void MenuButtonOnClicked(MenuActionType menuActionType)
        {
            MenuButtonClicked?.Invoke(menuActionType);
            this.gameObject.SetActive(false);
        }
    }
}