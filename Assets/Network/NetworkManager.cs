using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {
	const string VERSION = "v0.0.1";
	public string RoomName = "CD";

	public string[] CharacterName;
	
	public GameObject[] characters;
	public GameObject SelectedCharacter;
	public int SelectedCharacterIndex = 0;
	
	public GameObject ImageTarget;
	
	public Transform P1SpawnPoint;
	public Transform P2SpawnPoint;
	
	public Vector3 CharacterScale;

	private CharacterHealth CH;
	
	void Start () 
	{
		PhotonNetwork.ConnectUsingSettings(VERSION);
		SelectedCharacterIndex = PlayerPrefs.GetInt ("SelectedCharacter");

		if(SelectedCharacterIndex == 0) 
		{
			CharacterScale.x = 100;
			CharacterScale.y = 100;
			CharacterScale.z = 100;
		}
		else if(SelectedCharacterIndex == 1) 
		{
			CharacterScale.x = 80;
			CharacterScale.y = 80;
			CharacterScale.z = 80;
		}
		CharacterName[0] = "Dragon";
		CharacterName[1] = "Lava Golem";
	}

	void Update ()
	{
	}

	void OnJoinedLobby ()
	{
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom(RoomName, roomOptions, TypedLobby.Default);
	}
	
	void OnJoinedRoom ()
	{

		if (PhotonNetwork.countOfPlayersInRooms <= 0) 
		{
			SelectedCharacter = (GameObject) PhotonNetwork.Instantiate (CharacterName [SelectedCharacterIndex], P1SpawnPoint.localPosition, P1SpawnPoint.localRotation, 0);
		}
		else if (PhotonNetwork.countOfPlayersInRooms >= 1) 
		{
			SelectedCharacter = (GameObject) PhotonNetwork.Instantiate (CharacterName [SelectedCharacterIndex], P2SpawnPoint.localPosition, P2SpawnPoint.localRotation, 0);
		}

		SelectedCharacter.transform.SetParent(ImageTarget.transform);
		SelectedCharacter.GetComponent<MeleeSystem> ().enabled = true;
		SelectedCharacter.GetComponent<CharacterHealth> ().enabled = true;
		SelectedCharacter.GetComponent<AccelerometerMovement> ().enabled = true;
	}
}

















