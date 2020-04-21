﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotesManager : MonoBehaviour
{
	public Note[] notes;

	private void Awake() {
		if (FindObjectsOfType<NotesManager>().Length > 1) {
			Destroy(gameObject);
		} else {
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
		StartCoroutine(VerifyNotes());
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		StartCoroutine(VerifyNotes());
	}

	IEnumerator VerifyNotes() {
		yield return new WaitForSeconds(0.05f);

		TeleportManager.placesName currentPlace = FindObjectOfType<TeleportManager>().currentPlace;

		foreach (Note note in notes) {
			if (note.placeName == currentPlace) {
				if (!note.isPlayer)
					note.gameObject.SetActive(true);
			} else {
				if (note)
					note.gameObject.SetActive(false);
			}
		}
	}

	private void OnDestroy() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
