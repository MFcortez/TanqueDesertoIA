using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("ai");
    }
    void Update()
    {
        //Ao clicar pega a posição do mouse no clique e passa como destino para o agente
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                foreach (GameObject a in agents)
                    a.GetComponent<AIControl>().agent.SetDestination(hit.point);
            }
        }
    }
}
