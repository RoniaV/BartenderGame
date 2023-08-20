using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class NPCState : Patterns.State
{
    protected NPCBase mNPC;
    private int mStateType;
    public int StateType {  get { return mStateType; } }
    public NPCState(FSM fsm, int type, NPCBase npc) : base(fsm)
    {
        mNPC = npc;
        mStateType = type;
    }

    // the delegate
    public delegate void StateDelegate();
    public StateDelegate OnEnterDelegate { get; set; } = null;
    public StateDelegate OnExitDelegate { get; set; } = null;
    public StateDelegate OnUpdateDelegate { get; set; } = null;
    public StateDelegate OnFixedUpdateDelegate { get; set; } = null;

    public override void Enter()
    {
        OnEnterDelegate?.Invoke();
    }

    public override void Exit()
    {
        OnExitDelegate?.Invoke();
    }

    public override void Update()
    {
        OnUpdateDelegate?.Invoke();
    }

    public override void FixedUpdate()
    {
        OnFixedUpdateDelegate?.Invoke();
    }
}
