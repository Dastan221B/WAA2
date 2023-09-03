using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private ProgramState _programState;
    [SerializeField] private GameObject _loadScreen;
    [SerializeField] private Image _loadScreenImage;
    [SerializeField] private Sprite[] _loadScreens;
    [SerializeField] private Image _loadbar;
    [SerializeField] private TMP_Text _enterText;
    private StatesOfProgram _statesOfProgram;
    public bool IsLoaded { get; private set; }
    private System.Action _actionBeforeLoadig;

    public void OpenLoadBar(StatesOfProgram statesOfProgram, System.Action action = null)
    {
        if (action != null)
            _actionBeforeLoadig = action;
        _statesOfProgram = statesOfProgram;
        _loadScreen.SetActive(true);
        _loadScreenImage.sprite = _loadScreens[Random.Range(0, _loadScreens.Length)];
        _programState.LoadingStartHandler();
        StartCoroutine(Loading());
    }

    private void Update()
    {
        if (IsLoaded)
        {
            if (Input.anyKeyDown)
            {
                Close();
            }
        }
    }

    private void Close()
    {
        IsLoaded = false;
        _enterText.text = "";
        _loadScreen.SetActive(false);
        _programState.SetStatesOfProgram(_statesOfProgram);
        //_actionBeforeLoadig?.Invoke();
    }

    private IEnumerator Loading()
    {
        _loadbar.fillAmount = 0;
        float count = 0;
        while (true)
        {

            _loadbar.fillAmount = count/100;
            count++;
            if (count >= 100)
                break;
            yield return new WaitForSeconds(0.005f);
        }
        IsLoaded = true;
        _enterText.text = "Нажмите на любую клавишу...";
    }



}
