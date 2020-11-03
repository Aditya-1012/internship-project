using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class uimanager : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField playerfield=null;
    public Button enterbtn=null;
    public const string playerprefskey = "playername";
    public GameObject opponentpanel;
    public GameObject instructionpanel;
    void Start()
    {
        instructionpanel.SetActive(true);
        enteringname();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();

        }
    }
    public void enteringname()
    {
        if(!PlayerPrefs.HasKey(playerprefskey))
        {
            return;
        }
        
        string defaultname = PlayerPrefs.GetString(playerprefskey);
        playerfield.text = defaultname;
        savingplayer(defaultname);
    }
    public void savingplayer(string name)
    {
        enterbtn.interactable = !string.IsNullOrEmpty(name);
        
    }
    public void playersaved()
    {
        string playername = playerfield.text;
        PhotonNetwork.NickName = playername;
        PlayerPrefs.SetString(playerprefskey, playername);
        opponentpanel.SetActive(true);
        instructionpanel.SetActive(false);

    }
    public void onqiut()
    {
        Application.Quit();
    }
    public void onpressinstruction()
    {
        instructionpanel.SetActive(true);
    }
    public void onpressclose()
    {
        instructionpanel.SetActive(false);
    }
}
