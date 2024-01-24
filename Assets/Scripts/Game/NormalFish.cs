using System;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace daifuDemo
{
	public partial class NormalFish : ViewController
	{
		private bool _ifPlayersNearby = false;

		private float _swimTime = 3.0f;

		private float _swimRate = 2.0f;

		private Vector2 _direction;

		private Vector2 _startPosition;
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				_ifPlayersNearby = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				_ifPlayersNearby = false;
			}
		}

		private void Awake()
		{
			_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			_startPosition = transform.position;
		}

		private void Update()
		{
			if (Vector2.Distance(transform.position, _startPosition) >= 10f)
			{
				_direction = -_direction;
			}
			
			_swimTime -= Time.deltaTime;
			if (_swimTime <= 0)
			{
				_swimTime = 3.0f;
				_direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
			}

			if (_ifPlayersNearby)
			{
				var playerPosition = FindObjectOfType<Player>().transform.position;
				_direction = (transform.position - playerPosition).normalized;
				_swimRate = 5.0f;
			}
			else
			{
				_swimRate = 3.0f;
			}
			
			var swimSpeed = _swimRate * _direction;
			var position = transform.position;
			transform.position = new Vector3(position.x + swimSpeed.x * Time.deltaTime,
				position.y + swimSpeed.y * Time.deltaTime, position.z);
		}
	}
}
