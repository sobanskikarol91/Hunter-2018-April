﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

        if (firstMenuToDisplay != MENU.None)
            SwitchToMenu(firstMenuToDisplay);
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
        currentMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

public enum MENU { GameOver, Game, LvlsPanel, MainMenu, None }

