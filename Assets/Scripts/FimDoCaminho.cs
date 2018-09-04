using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FimDoCaminho : MonoBehaviour {
	
	[SerializeField] private Jogador jogador;

	void OnTriggerEnter (Collider collider) {

		if (collider.CompareTag ("Inimigo")) {
			Destroy (collider.gameObject);
			jogador.PerdeVida ();
		}
	}
}
