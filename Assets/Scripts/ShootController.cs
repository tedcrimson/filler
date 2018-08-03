using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public delegate void Hit(ShootController c);
	public static event Hit OnHit;


	public GameObject defaultRenderer;
	public ParticleSystem GoodParticle;
	public ParticleSystem PerfectParticle;

	private Animator animator;
	private Rigidbody2D rigid;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		animator = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		LevelManager.OnStateCheck += Animate;
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		LevelManager.OnStateCheck -= Animate;
		
	}



	void OnTriggerEnter2D(Collider2D other)
	// void OnCollisionEnter2D(Collision2D other)
	{
	
		Debug.LogError(other.gameObject.name);
		if(other.gameObject.tag != "Hit")
			OnHit(this);
	}

	public void Animate(int state)
	{
		// if(state != State.BAD)
		animator.SetTrigger(((State)state).ToString());
	}

	public void Throw()
	{
		Debug.Log("thww");
		rigid.velocity = Vector2.up * 15;
	}
	
	public void FixPosition(GameObject g)
	{
		this.transform.parent = g.transform;
		this.transform.localPosition = Vector2.zero;
		this.transform.localEulerAngles = Vector2.zero;
		rigid.velocity = Vector2.zero;
		rigid.simulated = false;
		defaultRenderer.SetActive(false);// = false;
	}

	public void GravityOff()
	{
		rigid.simulated = false;
		rigid.velocity = Vector2.zero;
	}

	public void DestroyObject()
	{
		Destroy(this.gameObject);
	}

	public void PlayPerfect()
	{
		if(PerfectParticle != null)
			PerfectParticle.Play();
	}

	public void PlayGood()
	{
		if(GoodParticle != null)
			GoodParticle.Play();
	}
}
