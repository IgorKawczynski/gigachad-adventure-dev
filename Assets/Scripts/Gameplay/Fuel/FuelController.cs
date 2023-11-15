using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    [SerializeField] private Image _fuelImage; // image for fuel 
    [SerializeField, Range(0.1f, 5f)] private float _fuelDrainSpeed = 1f; // u can edit the time for how fuel drains fully
    [SerializeField] private float _maxFuelAmount = 100f;
    [SerializeField] private Gradient _fuelGradient;

    private float _currentFuelAmount;

    // method initalized right after creating the instance (before method "Start")
    private void Awake() {
        if(instance==null) {
            instance=this;
        }
    }
    
    private void Start() {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }

    private void Update() {
        _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
        UpdateUI();
        if(_currentFuelAmount <= 0f) {
            GameManager.instance.GameOver();
        }
    }

    private void UpdateUI() {
        if (_fuelImage != null && _fuelGradient != null) {
            _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
            _fuelImage.color = _fuelGradient.Evaluate(_fuelImage.fillAmount);
        }
    }

    public void Refill() {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }
}
