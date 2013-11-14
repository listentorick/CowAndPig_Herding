using UnityEngine;

[System.Serializable]
public class Momentifier {
	public float minValue = -1f;	// Lowest value of variable.
	public float maxValue = 1f;		// Highest value of variable.
	public float attack = 1f;		// Amount of growth per input unit per second.
	public float decay = 1f;		// Amount of loss per input unit per second.

	float current;
	float lastSampleTime;
	
	
	public Momentifier(float minValue, float maxValue, float attack, float decay) {
		this.minValue = minValue;
		this.maxValue = maxValue;
		this.attack = attack;
		this.decay = decay;
	}
	

//	Receive an input and return the current variable value. 
	public float GetValue(float inputVal) {
		if (inputVal == 0) {
			if (Mathf.Sign(current) > 0) {
				current = Mathf.Max(0, current - decay * (Time.time - lastSampleTime));
			} else {
				current = Mathf.Min(0, current + decay * (Time.time - lastSampleTime));
			}
		} else {
			current = Mathf.Clamp(current + inputVal * Time.deltaTime * attack, minValue, maxValue);
		}
		
		lastSampleTime = Time.time;
		return current;
	}
}
