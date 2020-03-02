using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTrigger : MonoBehaviour
{
    [SerializeField] private string superscription;
    [SerializeField] private string virusName;
    [TextArea]
    [SerializeField] private string info;
    [TextArea]
    [SerializeField] private string postScriptum;

    [SerializeField] private int showTimes = 1;
    private int currentShows = 0;


    public void ShowInfoPanel()
    {
        if (currentShows < showTimes)
        {
            currentShows++;
            HeadPanelController.Instance.infoPanel.ShowInfoPanel(superscription, virusName, info, postScriptum);
        }
    }

}
