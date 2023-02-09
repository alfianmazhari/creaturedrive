using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

	Vector3 RealPosition = Vector3.zero;
	Quaternion RealRotation = Quaternion.identity;

	Animator Anim;

	void Start () 
	{
		Anim = GetComponent<Animator> ();
		if (Anim == null) 
		{
			Debug.LogError ("There is no Animator on this character prefab!");
		}
	}

	void Update()
	{
		if (photonView.isMine) 
		{
		} 
		else 
		{
			transform.position = Vector3.Lerp(transform.position, RealPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, RealRotation, 0.1f);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) 
		{
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);

			stream.SendNext (Anim.GetBool("Run"));
			stream.SendNext (Anim.GetBool("Attack"));
			stream.SendNext (Anim.GetBool("Drop"));
			stream.SendNext (Anim.GetBool("PowerAttack"));
			stream.SendNext (Anim.GetBool("SpecialAttack"));
		} 
		else 
		{
			RealPosition = (Vector3) stream.ReceiveNext();
			RealRotation = (Quaternion) stream.ReceiveNext();

			this.Anim.SetBool("Run", (bool)stream.ReceiveNext());
			this.Anim.SetBool("Attack", (bool)stream.ReceiveNext());
			this.Anim.SetBool("Drop", (bool)stream.ReceiveNext());
			this.Anim.SetBool("PowerAttack", (bool)stream.ReceiveNext());
			this.Anim.SetBool("SpecialAttack", (bool)stream.ReceiveNext());
		}
	}
}
