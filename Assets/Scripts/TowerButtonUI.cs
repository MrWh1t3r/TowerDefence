using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerButtonUI : MonoBehaviour
{
 public TowerData tower;
 public TextMeshProUGUI towerNameText;
 public TextMeshProUGUI towerCostText;
 public Image towerIcon;

 private Button _button;

 private void OnEnable()
 {
  GameManager.instance.onMoneyChanged.AddListener(onMoneyChanged);
 }

 private void OnDisable()
 {
  GameManager.instance.onMoneyChanged.RemoveListener(onMoneyChanged);
 }

 private void Awake()
 {
  _button = GetComponent<Button>();
 }

 public void onClick()
 {
  GameManager.instance.towerPlacement.SelectTowerToPlace(tower);
 }

 private void Start()
 {
  towerNameText.text = tower.displayName;
  towerCostText.text = $"${tower.cost}";
  towerIcon.sprite = tower.icon;
  
  onMoneyChanged();
 }

 void onMoneyChanged()
 {
  _button.interactable = GameManager.instance.money >= tower.cost;
 }
}

