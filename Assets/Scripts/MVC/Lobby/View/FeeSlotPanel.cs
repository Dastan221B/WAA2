using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class FeeSlotPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private TMP_InputField _feeTextField;
    [SerializeField] private Button _increaseButton;
    [SerializeField] private Button _deacreseButton;
    private IUpdateFeeMap _updateFeeMap;
    private ParticipantSlot _participantSlot;
    private Coroutine _onChangeFeeCoroutine;

    private void Awake()
    {
        _increaseButton.onClick.AddListener(IncreaseFee);
        _deacreseButton.onClick.AddListener(DecreaseFee);
        _feeTextField.onValueChanged.AddListener(OnChangeFEE);
    }

    public void Init(IUpdateFeeMap updateFeeMap, ParticipantSlot participantSlot)
    {
        _updateFeeMap = updateFeeMap;
        _participantSlot = participantSlot;
    }

    public void OnChangeFEE(string value)
    {
        string valueWithoutFEE = value.Replace("FEE:", "");
        _feeTextField.text = valueWithoutFEE;
        Debug.Log(valueWithoutFEE);
        _onChangeFeeCoroutine = StartCoroutine(TimeToChangeFEE(valueWithoutFEE));
        if (_onChangeFeeCoroutine != null)
            StopCoroutine(_onChangeFeeCoroutine);

        
    }

    private IEnumerator TimeToChangeFEE(string value)
    {
        yield return new WaitForSeconds(1);
        _updateFeeMap.UpdateFeeMap(_participantSlot.Ordinal, int.Parse(value));
        _feeTextField.text = $"FEE:{value}";
    }

    public void UpdateFeeText(int feeCount)
    {
        _feeTextField.text =$"FEE:{feeCount}";
    }

    private void DecreaseFee()
    {
        _updateFeeMap.UpdateFeeMap(_participantSlot.Ordinal, DecreaseIncreaseMode.Decrease);
    }
    private void IncreaseFee()
    {
        _updateFeeMap.UpdateFeeMap(_participantSlot.Ordinal, DecreaseIncreaseMode.Increase);
    }
}