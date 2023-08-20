using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class State
    {
        protected FSM m_fsm;
        /* The constructor of the State class
         * will require the parent FSM.
         * So we create a constructor 
         * with an instance of the FSM
         */
        public State(FSM fsm)
        {
            m_fsm = fsm;
        }
        /*!Virtual method for entry to the state.
         * This method is called whenever this 
         * state is entered. Derived classes
         * must implement this method and 
         * handle appropriately.
         */
        public virtual void Enter() { }
        /*!Virtual method for exit from the state.
         * This method is called whenever this 
         * state is exited. Derived classes
         * must implement this method and 
         * handle appropriately.
         */
        public virtual void Exit() { }
        /*!Virtual method that will be 
         * called in every Update call from Unity.
         * The call will be routed via the
         * FSM through the current state.
         */
        public virtual void Update() { }
        /*!Virtual method that will be 
         * called in every FixedUpdate call from 
         * Unity. The call will be routed via the
         * FSM through the current state.
         */
        public virtual void FixedUpdate() { }
    }

    public class FSM
    {
        protected Dictionary<int, State> m_states;
        protected State m_currentState;

        public FSM()
        {
            m_states = new Dictionary<int, State>();
        }

        public void Add(int key, State state)
        {
            m_states.Add(key, state);
        }

        public State GetState(int key)
        {
            return m_states[key];
        }

        public void SetCurrentState(State state)
        {
            if (m_currentState != null)
            {
                m_currentState.Exit();
            }

            m_currentState = state;

            if (m_currentState != null)
            {
                m_currentState.Enter();
            }
        }

        public void Update()
        {
            if (m_currentState != null)
            {
                m_currentState.Update();
            }
        }

        public void FixedUpdate()
        {
            if (m_currentState != null)
            {
                m_currentState.FixedUpdate();
            }
        }
    }
}
