using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogo : MonoBehaviour {

	[SerializeField] private GameObject torrePrefab;
	[SerializeField] private GameObject gameOver;
	[SerializeField] private Jogador jogador;
    [SerializeField] private GameObject botaoSair;
	[Range (1, 10)]
	public int maximoTorres;
	[HideInInspector] public int numAtualTorres;
    [HideInInspector] public int aumentou = 0;

	void Start () {

		gameOver.SetActive (false);
		Time.timeScale = 1; // Retomar a movimentação na cena

        #if UNITY_STANDALONE || UNITY_EDITOR
                botaoSair.SetActive(true);
        #endif
    }

    void Update () {

		if (JogoAcabou ()) {
			gameOver.SetActive (true);
			Time.timeScale = 0; // Pausar toda movimentação na cena
		} else {
			if (ClicouComBotaoPrimario ()) {
				ConstroiTorre ();
			}
		}
	}

	private bool JogoAcabou () {
		return !jogador.EstaVivo ();
	}

	public void ReiniciaJogo () {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void SaiDoJogo ()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public int GetNumeroTorres () {
		return maximoTorres - numAtualTorres;
	}

    public void AumentarMaximoTorres()
    {
        if (GetNumeroTorres() == 0 && aumentou == 0)
        {
            maximoTorres++;
            aumentou++;
        }
        if (GetNumeroTorres() == 0 && aumentou == 2)
        {
            maximoTorres++;
            aumentou++;
        }
    }

	// Checa se houve clique com botão primario:
	private bool ClicouComBotaoPrimario () {
		return Input.GetMouseButtonDown (0);
	}

	// Pega posição do clique, obtém informações do objeto que foi clicado e instancia uma torre naquele local:
	private void ConstroiTorre () {

		if (numAtualTorres < maximoTorres) {		// Limitar o número de torres criadas
			
			Vector3 posicaoDoClique = Input.mousePosition;
			RaycastHit elementoAtingidoPeloRaio = RaioDaCameraAoClique (posicaoDoClique);

			if (elementoAtingidoPeloRaio.collider != null
			    &&
			    elementoAtingidoPeloRaio.collider.CompareTag ("Terreno")) {

				numAtualTorres++;
				Vector3 posicaoDeCriacaoDaTorre = elementoAtingidoPeloRaio.point;
				Instantiate (torrePrefab, posicaoDeCriacaoDaTorre, torrePrefab.transform.rotation);
			}
		} else {
			Debug.Log ("Número máximo de torres atingido!");
		}
	}

	// Dispara um raio da camera até o ponto de clique, para pegar informações do elemento (objeto) que foi clicado:
	private RaycastHit RaioDaCameraAoClique (Vector3 posicaoDoClique) {
		
		Ray raio = Camera.main.ScreenPointToRay (posicaoDoClique);
		float comprimentoMaximoDoRaio = 100.0f; // Importante para restringir de quais elementos pegar informação
												// (OBS: Valor alto pode detectar mais de um objeto atravessado pelo raio)
		RaycastHit elementoAtingidoPeloRaio;

		Physics.Raycast (raio, out elementoAtingidoPeloRaio, comprimentoMaximoDoRaio); // Elemento atingido como referência: 'out'

		return elementoAtingidoPeloRaio;
	}
}