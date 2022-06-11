using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovingObstacle : MonoBehaviour
{
    public enum AfterAction { MoveBackward, MoveToFirstPoint, InstantlyTransformToFirstPoint };
    public AfterAction AfterReachedLast;
    public float movingSpeed;
    public bool waitBetweenPoints;
    [HideInInspector] [SerializeField] Vector3[] points;
    [HideInInspector] [SerializeField] float seconds;
    private int IND;
    private bool increasing;
    private bool positionReached;
    private float timer;
    void Start()
    {
        transform.localPosition = points[0];    //starting position should match the first element in points array
    }

    // Update is called once per frame
    void Update()
    {
        if(AbilityController.instance.IsFreezeActive()) //if freeze ability is active the obstacle will not move
        {
            return;
        }
        if(!positionReached)    //if obstacle still not reached to the next point keep moving
        {
            MoveObj();
            return;
        }
        if(timer < seconds) //if the obstacle reached to the next point wait for time amount of seconds (specified in the inspector)
        {
            timer += Time.deltaTime;
            return;
        }
        if(AfterReachedLast == AfterAction.MoveBackward)    //after obtacle reached to the last point what should it do
        {
            MoveBackwardAfterFinish();
        }
        else if(AfterReachedLast == AfterAction.MoveToFirstPoint)
        {
            MoveToFirstPointAfterFinish();
        }
        else if(AfterReachedLast == AfterAction.InstantlyTransformToFirstPoint)
        {
            TransformToFirstPointAfterFinsh();
        }
        timer = 0f;
        positionReached = false;
    }
    private void MoveObj()      //move the obstacle
    {
        float step = movingSpeed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, points[IND], step);
        if(Mathf.Approximately(Vector3.Distance(transform.localPosition, points[IND]),0))   //if obstacle reached to the next point
        {
            transform.localPosition = points[IND];  //make sure that the obstacle position is equal the point
            positionReached = true;
        }
    }
    private void MoveBackwardAfterFinish()
    {
        if (increasing)
        {
            if (IND != points.Length - 1)
            {
                IND++;
            }
            else
            {
                IND--;
                increasing = false;
            }
        }
        else
        {
            if (IND != 0)
            {
                IND--;
            }
            else
            {
                IND++;
                increasing = true;
            }
        }
    }
    private void MoveToFirstPointAfterFinish()
    {
        if(IND < points.Length - 1)
        {
            IND++;
        }
        else
        {
            IND = 0;
        }
    }
    private void TransformToFirstPointAfterFinsh()
    {
        if (IND < points.Length - 1)
        {
            IND++;
        }
        else
        {
            IND = 0;
            transform.localPosition = points[0];
        }
    }
#if UNITY_EDITOR

    [CustomEditor(typeof(MovingObstacle))]
    public class MovingObstacleEditor : Editor  //Editor class to hide and display fields based on conditions in the inspector
    {
        public override void OnInspectorGUI()   //override OnInspectorGUI function from the base
        {
            MovingObstacle MovingSC = (MovingObstacle)target;    //create instance on the targeted class

            base.OnInspectorGUI();      //call the base function to draw MovingObstacle script variables in the inspector

            DrawSeconds(MovingSC);

            DrawPointsArray(MovingSC);

            EditorUtility.SetDirty(MovingSC);      //This will turn on saving for the added variables values on the
                                                //inspector when switching scenes
        }

        static void DrawSeconds(MovingObstacle MovingSC)
        {
            if (!MovingSC.waitBetweenPoints)  
            {
                MovingSC.seconds = 0;
                return;
            }

            EditorGUILayout.Space();            ///Adding space in the inspector

            EditorGUILayout.BeginHorizontal();      //start horizontal gui elements drawing
            //{
            EditorGUILayout.LabelField("Seconds to wait", GUILayout.MaxWidth(95));   //Add label with max width 95
            MovingSC.seconds = EditorGUILayout.Slider(MovingSC.seconds, 0f, 10f); //Added slider to assign seconds variable
            //}
            EditorGUILayout.EndHorizontal();    //end horizontal gui elements drawing

        }

        void DrawPointsArray(MovingObstacle MovingSC)
        {

            SerializedProperty tps = serializedObject.FindProperty("points");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(tps, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
