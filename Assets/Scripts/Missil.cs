using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : MonoBehaviour {

	private float velocidade = 10;
	private Inimigo alvo;

	[Range(0,5)]
	[SerializeField] private int pontosDeDano = 2;

	// Use this for initialization
	void Start () {
		
		AutoDestruicao (2);
	}

	// Update is called once per frame
	void Update () {
		
		Anda ();

		if (alvo != null) {
			AlteraPosicao ();
		}
	}

	// Move o míssil para a sua 'frente' orientada pelo equivalente a 10m a cada segundo:
	private void Anda () {
		
		Vector3 posicaoAtual = transform.position;
		Vector3 descolamento = transform.forward * Time.deltaTime * velocidade; // Efeito de 10m/s pela adição do 'Time.deltaTime'
		transform.position = posicaoAtual + descolamento;
	}

	// Corrige a posição que o míssil deve andar para a posição atual do alvo:
	private void AlteraPosicao () {
		
		Vector3 posicaoAtual = transform.position;
		Vector3 posicaoDoAlvo = alvo.transform.position;
		Vector3 direcaoDoAlvo = posicaoDoAlvo - posicaoAtual;
		transform.rotation = Quaternion.LookRotation (direcaoDoAlvo);
	}

	private void AutoDestruicao (float tempoDestruicao) {
		Destroy (this.gameObject, tempoDestruicao);
	}

	public void DefineAlvo (Inimigo inimigo) {
		alvo = inimigo;
	}

	// Ao colidir com inimigo, tira dano dele e destóri o míssil:
	private void OnTriggerEnter (Collider elementoColidido) {
		if (elementoColidido.CompareTag ("Inimigo")) {
			
			Destroy (this.gameObject);
			Inimigo inimigo = elementoColidido.GetComponent <Inimigo> ();
			inimigo.RecebeDano (pontosDeDano);
		}
	}
}
