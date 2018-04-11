using UnityEngine;
using System.Collections;
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

    [SerializeField] Menu[] menuList;
    [SerializeField] Dictionary<MENU, GameObject> menuDictionary = new Dictionary<MENU, GameObject>();
    GameObject currentMenu;
    GameObject previousMenu;

    protected override void Awake()
    {
        base.Awake();
        CreateDictionary();
        DisableMenus();
        SwitchToMenu(MENU.SelectLvl);
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
        if(currentMenu != null)
        {
            previousMenu = currentMenu;
            currentMenu.SetActive(false);
        }
        currentMenu = menuDictionary[showMenu];
        currentMenu.SetActive(true);
    }

    public void SwitchToSelectLvl()
    {
        SwitchToMenu(MENU.SelectLvl);
    }
}

public enum MENU { GameOver, Game, SelectLvl }

