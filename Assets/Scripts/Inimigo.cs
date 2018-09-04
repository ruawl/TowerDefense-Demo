using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour {
	
	[Range(0,20)]
	[SerializeField] private int vida = 10;
    private NavMeshAgent agente;
    private GameObject fimDoCaminho;
    
    void Start ()
    {
        agente = GetComponent<NavMeshAgent>();

        fimDoCaminho = GameObject.Find("FimDoCaminho");
        Vector3 posicaoFinal = fimDoCaminho.transform.position;

        if(agente.isOnNavMesh)
        {
            Debug.Log("ACERTOU");
            agente.SetDestination(posicaoFinal);
        }
        else
        {
            Debug.Log("ERROU");
        }
	}

	public void RecebeDano (int pontosDeDano) {
		
		vida -= pontosDeDano;
		if (vida <= 0) {
			Destroy (this.gameObject);
		}
	}
}
