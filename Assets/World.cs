using UnityEngine;

public class World : SingletonMonoBehaviour<World>
{
	[SerializeField]
	private float airResistance = 5.0f;
	public float AirResistance {
		get { return this.airResistance; }
		set { this.airResistance = value; }
	}
	[SerializeField]
	private float stopThreshold = 0.001f;
	public float StopThreshold {
		get { return this.stopThreshold; }
		set { this.stopThreshold = value; }
	}

	public void Awake()
	{
		if (this != Instance) {
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
