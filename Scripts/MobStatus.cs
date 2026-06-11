using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobStatus : MonoBehaviour
{
    protected enum StateEnum
    {
        Normal, 
        Attack,
        Die
    }

    public bool IsMovable => StateEnum.Normal == _state;
    public bool IsAttackable => StateEnum.Normal == _state;
    public float LifeMax => lifeMax;
    public float Life => _life;

    [SerializeField] private float lifeMax = 10;
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal;
    private float _life;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _life = LifeMax;
        _animator = GetComponentInChildren<Animator>();
    }

    //死亡時の処理
    protected virtual void OnDie()
    {
    }

    //指定値のダメージを受ける
    public virtual void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;

        if (_life > 0) 
            return;
        else
        {
            _life = 0;
            _state = StateEnum.Die;
            _animator.SetTrigger("Die");
            OnDie();
        }
    }

    //回復
    public void Heal(int heal)
    {
        if (_state == StateEnum.Die) return;

        _life = _life + heal > lifeMax ? lifeMax : _life + heal;//回復
    }

    //可能であれば攻撃の状態に移行する
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    //可能であればnormalの状態に移行する
    public void GoToNormalStateIfPossible()
    {
        if(_state == StateEnum.Die) return;
        _state = StateEnum.Normal;
    }
}
