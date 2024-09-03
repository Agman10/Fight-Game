using UnityEngine;

public class EpicBgController : MonoBehaviour
{
	public Material skybox;

	public Vector2 TimeFrame;

	public AudioSource audioSource;

	private bool skyboxDone;

	private float testTime = -0.5f;

	//private void Awake() => this.audioSource = GetComponent<AudioSource>();

	private void OnDestroy()
	{
		this.skybox.SetFloat("_Intro_T", 0.0f);
	}

	private void OnEnable()
	{
		
	}
	private void OnDisable()
	{
		this.skyboxDone = false;
		this.testTime = -0.5f;
		//this.testTime = 0f;
	}

	private void Update()
	{
		if (CharacterManager.Instance != null && CharacterManager.Instance.musicTypeId > 0 && !this.skyboxDone)
		{
			/*float lerpValue = Time.deltaTime;
			if (lerpValue == 7.0f)
				this.skyboxDone = true;

			this.skybox.SetFloat("_Intro_T", lerpValue);
			Debug.Log(lerpValue);*/

			this.testTime += Time.deltaTime;
			this.skybox.SetFloat("_Intro_T", this.testTime);


			//this.skybox.SetFloat("_Intro_T", 1f);
		}
		else if (this.audioSource != null && !this.skyboxDone)
		{
			float lerpValue = Mathf.InverseLerp(this.TimeFrame.x, this.TimeFrame.y, this.audioSource.time);

			this.skybox.SetFloat("_Intro_T", lerpValue);

			/*if (lerpValue == 1.0f)
				this.enabled = false;*/

			if (lerpValue == 1.0f)
				this.skyboxDone = true;

			//Debug.Log(lerpValue);
		}
		
	}
}
