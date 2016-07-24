using UnityEngine;

public class World : SingletonMonoBehaviour<World>
{
	[SerializeField]
	private float airResistance = 0.03f;
	[SerializeField]
	private float stopThreshold = 0.0001f;

	public void Awake()
	{
		if (this != Instance) {
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public float AirResistance { get; set; }
	public float StopThreshold { get; set; }

}
