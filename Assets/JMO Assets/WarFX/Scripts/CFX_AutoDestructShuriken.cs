using System;
using UnityEngine;
using System.Collections;
using _Scripts.Location;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	[SerializeField] private string _poolName;

	private void OnValidate()
	{
		_poolName = name;
	}

	public bool OnlyDeactivate;
	
	void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}

	private void OnDisable()
	{
		if (OnlyDeactivate)
		{
			var gamePool = SceneGamePool.Instance;
			if (gamePool != null)
			{
				gamePool.AddObjectInPool(_poolName, this.gameObject);
			}
		}
	}

	IEnumerator CheckIfAlive ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			if(!GetComponent<ParticleSystem>().IsAlive(true))
			{
				if(OnlyDeactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				else
					GameObject.Destroy(this.gameObject);
				break;
			}
		}
	}
}
