using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeInimigos : MonoBehaviour {

	[SerializeField] private GameObject inimigo;
	private float tempoDeGeracao = 2f;
	private float tempoUltimaGeracao;
    private Jogo jogo;

    private void Start()
    {
        this.jogo = FindObjectOfType<Jogo>();
    }

    // Update is called once per frame
    void Update () {

		// Aumenta o número de inimigos ao longo do tempo:
		if (Time.timeSinceLevelLoad > 30 && Time.timeSinceLevelLoad < 60) {
			tempoDeGeracao = 1f;
            jogo.AumentarMaximoTorres();
		} else if (Time.timeSinceLevelLoad > 60) {
			tempoDeGeracao = 0.5f;
            jogo.aumentou++;
            jogo.AumentarMaximoTorres();
        }

		//Debug.Log (Time.timeSinceLevelLoad);
		GeraInimigos ();
	}
		
	private void GeraInimigos () {
		
		float tempoAtual = Time.time;
		if (tempoAtual > tempoUltimaGeracao + tempoDeGeracao) {
			tempoUltimaGeracao = tempoAtual;
			Vector3 posicaoDeGeracao = this.transform.position;
			Instantiate (inimigo, posicaoDeGeracao, inimigo.transform.rotation);
		}
	}
}
