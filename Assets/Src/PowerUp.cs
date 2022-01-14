using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private ArkanoidController _arkanoidController;

    public List<int> ScoreTypes;
    private PowerUpData powerUpPrefab;
    private Dictionary<int, PowerUpData> _powerUpList;
    private int _powerUpCount;

    private void Start() {
        powerUpPrefab = Resources.Load<PowerUpData>("Prefabs/PowerUp");
        Init();
    }

    public void Init() {
        _powerUpList = new Dictionary<int, PowerUpData>();
        _powerUpCount = 0;
    }

    public void CreatePowerUp(Vector3 position) {
        PowerUpData powerUp = Instantiate<PowerUpData>(powerUpPrefab, transform);
        powerUp.transform.position = position;

        _powerUpCount += 1;
        powerUp.NewPowerUp(_powerUpCount);
        _powerUpList.Add(_powerUpCount, powerUp);
    }

    public void IsOut(int id) {
        _powerUpList.Remove(id);
        Debug.Log(_powerUpList);
    }

    public void TouchPaddle(int id) {
        PowerUpEffect(_powerUpList[id]);
        _powerUpList.Remove(id);
    }

    private void PowerUpEffect(PowerUpData powerUp) {
        if(powerUp.GetPType() == PType.small){
            _arkanoidController.Small();
        }else if(powerUp.GetPType() == PType.big){
            _arkanoidController.Big();
        }else if(powerUp.GetPType() == PType.slow){
            _arkanoidController.Slow();
        }else if(powerUp.GetPType() == PType.fast){
            _arkanoidController.Fast();
        }else if(powerUp.GetPType() == PType.points50){
            _arkanoidController.SumPoints(50);
        }else if(powerUp.GetPType() == PType.points100){
            _arkanoidController.SumPoints(100);
        }else if(powerUp.GetPType() == PType.points250){
            _arkanoidController.SumPoints(250);
        }else if(powerUp.GetPType() == PType.points500){
            _arkanoidController.SumPoints(500);
        }
    }

    public void ClearPowerUps() {
        List<int> powerKeys = new List<int>(_powerUpList.Keys);
        foreach (int i in powerKeys) {
            _powerUpList[i].DestroyPowerUp();
            _powerUpList.Remove(i);
        }
    }
}
