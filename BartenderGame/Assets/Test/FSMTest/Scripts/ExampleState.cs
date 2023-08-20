using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFSM;

public class ExampleState : MonoBehaviour
{

    /*[System.Serializable]
    public class Condition
    {
        public string parameter;
        public enum BoolCriteria {
            isTrue,
            isFalse,
        };
        public BoolCriteria boolCriteria;

        public enum IntCriteria
        {
            equal,
            lessThan,
            moreThan,
        };
        public IntCriteria inttCriteria;
        public int intValue;

        public enum FloatCriteria
        {
            equal,
            lessThan,
            moreThan,
        };
        public FloatCriteria floatCriteria;
        public float floatValue;


        public string valueForTrue;
    };*/

    [System.Serializable]
    public class Transition
    {
        public string name;
        public State destination;
        public Condition[] conditions;
    }

    public Transition[] transitions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
