using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuModel{

    private MenuController menuController;

    public MenuModel() { }

    public Image GetPanelOpts() { return menuController.panelOpts; }
    public Image GetPanelVS() { return menuController.panelVS; }
    public Image GetPanelMain() { return menuController.panelMain; }
}
