using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mission_Manager : MonoBehaviour
{
    private string current_objective;

    public List<string> demo_mission_objectives;

    public VisualElement hud;
    public Label objective_lable;


    // Start is called before the first frame update

    private void Awake()
    {
        hud = GetComponent<UIDocument>().rootVisualElement;
        objective_lable = hud.Q<Label>("Objective_text");
    }

    void Start()
    {
        if(demo_mission_objectives == null)
        {
            demo_mission_objectives= new List<string>();
        }

        if(demo_mission_objectives.Count > 0)
        {
            current_objective = demo_mission_objectives[0];
            objective_lable.text = current_objective;
        }
    }

    //public void OnGUI()
    //{
    //    if (GUILayout.Button("test next mission objective"))
    //    {
    //        InitiateNextDemoObjective();
    //    }
    //}

    public void InitiateNextDemoObjective(string NextObjective)
    {

        current_objective = NextObjective;
        objective_lable.text = current_objective;

        //if(current_objective != null && current_objective != "")
        //{
        //    currentobjectiveindex = demo_mission_objectives.IndexOf(current_objective);
        //    Debug.Log(currentobjectiveindex);

        //    if (demo_mission_objectives.Count -1 > currentobjectiveindex)
        //    {
        //        current_objective = demo_mission_objectives[currentobjectiveindex + 1];
        //        objective_lable.text = current_objective;
        //    }
        //    else Debug.LogWarning("No next objective");
        //}
        //else Debug.LogWarning("No current objective");

    }
    public void CrashReset()
    {
        current_objective = demo_mission_objectives[0];
        objective_lable.text = current_objective;
    }
}
