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

    //Faz o tanque se mover para o Heliporto
    public void GoToHeli()
    {
        //Chama o metodo AStar do Graph passando o ponto inicial e o destino para que seja gerado o caminho
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    //Faz o tanque se mover para as Ruinas
    public void GoToRuin()
    {
        //Chama o metodo AStar do Graph passando o ponto inicial e o destino para que seja gerado o caminho
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }

    //Faz o tanque se mover para a Fabrica
    public void GoToUsina()
    {
        //Chama o metodo AStar do Graph passando o ponto inicial e o destino para que seja gerado o caminho
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }

    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        //Define um destino atual para o tanque pegando como base o ponto atual
        currentNode = g.getPathPoint(currentWP);
        //Verifica se o tanque está próximo do nó de destino atual
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }

        if (currentWP < g.getPathLength())
        {
            //Define proximo ponto alvo do movimento
            goal = g.getPathPoint(currentWP).transform;
            //Define a direção do objeto destino
            Vector3 lookAtGoal = new Vector3(goal.position.x,
                this.transform.position.y,
                goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            //Move o objeto para a frente
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //Rotaciona o objeto conforme o destino
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);
        }
    }
}