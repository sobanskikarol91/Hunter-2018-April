using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class MenuManager : Singleton<MenuManager>
{
    [System.Serializable]
    public class Menu
    {
        public GameObject menu;
        public MENU type;
    }

    [SerializeField] MENU firstMenuToDisplay;
    [SerializeField] Menu[] menuList;
    [SerializeField] Dictionary<MENU, GameObject> menuDictionary = new Dictionary<MENU, GameObject>();
    GameObject currentMenu;

    protected override void Awake()
    {
        base.Awake();
        CreateDictionary();
        DisableMenus();
    }

    private void Start()
    {
        if (firstMenuToDisplay != MENU.None)
            SwitchToMenu(firstMenuToDisplay);
        else // TEST
        {
            BowEventManager.instance.Invoke("StartGame", 0.5f);

        }
    }

    void CreateDictionary()
    {
        foreach (Menu m in menuList)
            menuDictionary.Add(m.type, m.menu);
    }

    void DisableMenus()
    {
        for (int i = 0; i < menuDictionary.Count; i++)
            menuDictionary.ElementAt(i).Value.SetActive(false);
    }

    public void SwitchToMenu(MENU showMenu)
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }

        currentMenu = menuDictionary[showMenu];
        currentMenu.SetActive(true);
    }

    public void SwitchToSelectLvl()
    {
        SwitchToMenu(MENU.LvlsPanel);
    }

    public void LvlRestart()
    {
        SwitchToMenu(MENU.Game);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SwitchToMainMenu()
    {
        SwitchToMenu(MENU.MainMenu);
    }
}

public enum MENU { GameOver, Game, LvlsPanel, MainMenu, None }

