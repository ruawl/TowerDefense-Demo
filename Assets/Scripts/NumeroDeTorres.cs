using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumeroDeTorres : MonoBehaviour {

	private Text campoTexto;
	[SerializeField] private Jogo jogo;

	// Use this for initialization
	void Start () {
		campoTexto = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		campoTexto.text = "Torres: " + jogo.GetNumeroTorres ();
	}
}
