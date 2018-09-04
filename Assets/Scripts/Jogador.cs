using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {

	[Range (0, 20)]
	[SerializeField] private int vida;

	public int GetVida () {
		return vida;
	}

	public void PerdeVida () {
		if (EstaVivo ()) {
			vida -= 1;
		}
	}

	public bool EstaVivo() {
		return vida > 0;
	}
}
