using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PType {
    small,
    big,
    slow,
    fast,
    points50,
    points100,
    points250,
    points500

}
public class PowerUpData : MonoBehaviour
{
    private string _spritePath = "Sprites/PowerUps";

    private PType _type;

    private int _id;

    private SpriteRenderer _renderer;
    private Collider2D _coll2d;

    void Start()
    {   
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _coll2d= GetComponent<Collider2D>();
        _coll2d.enabled = true;
        _type = (PType)Random.Range(0,9);;
        string path = GetPath();
        _renderer.sprite = Resources.Load<Sprite>(path);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _position = transform.position;
        _position.y -= 1.5f * Time.deltaTime;
        transform.position = _position;
    }

    private string GetPath() {

        string path = string.Empty;

        if(_type == PType.small){
            path = _spritePath + "/small";
        }else if(_type == PType.big){
            path = _spritePath + "/big";
        }else if(_type == PType.slow){
            path = _spritePath + "/slow";
        }else if(_type == PType.fast){
            path = _spritePath + "/fast";
        }else if(_type == PType.points50){
            path = _spritePath + "/points50";
        }else if(_type == PType.points100){
            path = _spritePath + "/points100";
        }else if(_type == PType.points250){
            path = _spritePath + "/points250";
        }else if(_type == PType.points500){
            path = _spritePath + "/points500";
        }

        return path;

    }

    public void NewPowerUp(int id) {
        _id = id;
    }

    public void DestroyPowerUp() {
        _coll2d.enabled = false;
        Destroy(gameObject);
    }

    public int GetPowerUpId() {
        return _id;
    }
    public PType GetPType() {
        return _type;
    }

}
