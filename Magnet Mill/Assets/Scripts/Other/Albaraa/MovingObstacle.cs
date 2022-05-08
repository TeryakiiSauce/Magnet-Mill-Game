using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovingObstacle : MonoBehaviour
{
    public enum MovingType {MoveToPosition, MoveBy}
    public enum AfterAction { MoveBackward, MoveToFirstPoint, InstantlyTransformToFirstPoint };
    public MovingType movingMethod;
    public AfterAction AfterReachedLast;
    public float movingSpeed;
    public bool RotateWithDirection;
    public bool waitBetweenPoints;
    [HideInInspector] [SerializeField] Vector3[] points;
    [HideInInspector] [SerializeField] float seconds;
    private Vector3[] calculatedPoints;
    private int IND;
    private bool increasing;
    private bool positionReached;
    private float timer;
    void Start()
    {
        if(movingMethod == MovingType.MoveToPosition)
        {
            transform.position = points[0];
            calculatedPoints = points;
        }
        else
        {
            calculatedPoints = new Vector3[points.Length];
            for(int i = 0; i < points.Length; i++)
            {
                if(i == 0)
                {
                    calculatedPoints[i] = new Vector3(transform.position.x + points[i].x, transform.position.y + points[i].y
                    + transform.position.z + points[i].z);
                }
                else
                {
                    calculatedPoints[i] = new Vector3(calculatedPoints[i -1].x + points[i].x,
                        calculatedPoints[i - 1].y + points[i].y, calculatedPoints[i - 1].z + points[i].z);
                }            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!positionReached)
        {
            MoveObj();
            return;
        }
        if(timer < seconds)
        {
            timer += Time.deltaTime;
            return;
        }
        if(AfterReachedLast == AfterAction.MoveBackward)
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
    private void MoveObj()
    {
        float step = movingSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, calculatedPoints[IND], step);
        if(Mathf.Approximately(Vector3.Distance(transform.position, calculatedPoints[IND]),0))
        {
            transform.position = calculatedPoints[IND];
            positionReached = true;
        }
        else if(RotateWithDirection)
        {
            transform.LookAt(calculatedPoints[IND]);
        }
    }
    private void MoveBackwardAfterFinish()
    {
        if (increasing)
        {
            if (IND != calculatedPoints.Length - 1)
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
        if(IND < calculatedPoints.Length - 1)
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
        if (IND < calculatedPoints.Length - 1)
        {
            IND++;
        }
        else
        {
            IND = 0;
            transform.position = calculatedPoints[0];
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

            DrawDetails(MovingSC);

            DrawPointsArray(MovingSC);

            EditorUtility.SetDirty(MovingSC);      //This will turn on saving for the added variables values on the
                                                //inspector when switching scenes
        }

        static void DrawDetails(MovingObstacle MovingSC)
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
