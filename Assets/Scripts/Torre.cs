using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour {

	[SerializeField] private GameObject projetilPrefab;
	private float tempoUltimoDisparo;

	[Range(0,3)]										// [Range(x, y)] Define limites para alteração dinâmica do valor da variável
	[SerializeField] private int tempoRecarga; 			// [SerializeField] Deixa privado, mas ainda acessível na interface

	[Range(1, 10)]
	[SerializeField] private int raioDeAlcance;

	// Update is called once per frame
	void Update () {
		
		Inimigo alvo = EscolheAlvo ();

		if (alvo != null) {
			Atira (alvo);
		}
	}

	// Instancia um míssil para ser disparado e passa informações sobre qual inimigo ele deverá seguir:
	private void Atira (Inimigo inimigo) {
		
		float tempoAtual = Time.time;
		if (tempoAtual > tempoUltimoDisparo + tempoRecarga) {
			
			tempoUltimoDisparo = tempoAtual;
			GameObject pontoDeDisparo = this.transform.Find ("PontoDeDisparo").gameObject;
			Vector3 posicaoDisparo = pontoDeDisparo.transform.position;
			GameObject projetilObject = (GameObject) Instantiate (projetilPrefab, posicaoDisparo, Quaternion.identity);

			// Passa para o míssil o inimigo que ele deverá seguir:
			Missil missil = projetilObject.GetComponent <Missil> ();
			missil.DefineAlvo(inimigo);
		}
	}

	// Escolhe sempre o primeiro inimigo a passar dentro do alcance da torre:
	private Inimigo EscolheAlvo () {
		
		GameObject[] inimigos = GameObject.FindGameObjectsWithTag ("Inimigo");
		foreach (GameObject inimigo in inimigos) {
			if (EstaDentroDoAlcance (inimigo)) {
				return inimigo.GetComponent <Inimigo> ();
			}
		}
		return null;
	}

	// Checa se o inimigo está dentro do alcance definido para a torre:
	private bool EstaDentroDoAlcance (GameObject inimigo) {
		
		Vector3 posicaoTorreNoPlano = Vector3.ProjectOnPlane(this.transform.position, Vector3.up);
		Vector3 posicaoInimigoNoPlano = Vector3.ProjectOnPlane(inimigo.transform.position, Vector3.up);

		float distanciaInimigo = Vector3.Distance (posicaoTorreNoPlano, posicaoInimigoNoPlano);

		return distanciaInimigo <= raioDeAlcance;
	}
}
