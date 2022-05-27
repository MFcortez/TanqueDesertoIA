using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowPath : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }
    public void GoToHeli()
    {
        //Passa ao método os pontos atuais e alvo para mover o agente [1]
        g.AStar(currentNode, wps[1]);
        //Zera o contador de movimento
        currentWP = 0;
    }
    //Método para se mover ao ponto ruina
    public void GoToRuin()
    {
        //Passa ao método os pontos atuais e alvo para mover o agente [6]
        g.AStar(currentNode, wps[6]);
        //Zera o contador de movimento
        currentWP = 0;
    }

    public void GoToUsina()
    {
        //Passa ao método os pontos atuais e alvo para mover o agente [6]
        g.AStar(currentNode, wps[9]);
        //Zera o contador de movimento
        currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);
        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if (Vector3.Distance(
        g.getPathPoint(currentWP).transform.position,
        transform.position) < accuracy)
        {
            currentWP++;
        }

        if (currentWP < g.getPathLength())
        {
            //Define proximo ponto alvo do movimento
            goal = g.getPathPoint(currentWP).transform;
            //Aloca próximo ponto em um vetor
            Vector3 lookAtGoal = new Vector3(goal.position.x,
            this.transform.position.y,
            goal.position.z);
            //Utiliza o vetor para rotacionar em direção ao alvo
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //Rotaciona e move o objeto
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);
        }
    }
}