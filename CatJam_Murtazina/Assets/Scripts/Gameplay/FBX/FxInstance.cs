using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class FxInstance : MonoBehaviour, IPoolable<Vector3, Quaternion, IMemoryPool>, IDisposable
    {
        [Header("Optional")]             
        [SerializeField] private ParticleSystem[] _particles;       
        [SerializeField] private float _overrideDuration;      

        private IMemoryPool _pool;
        private Coroutine _lifeRoutine;

        public void OnSpawned(Vector3 position, Quaternion rotation, IMemoryPool pool)
        {
            _pool = pool;
            transform.SetPositionAndRotation(position, rotation);
            gameObject.SetActive(true);
         
            if (_particles != null)
                foreach (var particle in _particles)
                {
                    if (particle)
                    {
                        particle.Clear(true); 
                        particle.Play(true);
                    }
                }
           
            var duration = CalcDuration();
            _lifeRoutine = StartCoroutine(LifeTimer(duration));
        }

        public void OnDespawned()
        {
            if (_lifeRoutine != null) StopCoroutine(_lifeRoutine);
            _lifeRoutine = null;

            if (_particles != null)
                foreach (var p in _particles) if (p) p.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            gameObject.SetActive(false);
            _pool = null;
        }

        public void Dispose() => Despawn();

        private IEnumerator LifeTimer(float seconds)
        {
            if (seconds <= 0f) seconds = 0.75f;
            yield return new WaitForSeconds(seconds);
            Despawn();
        }

        private void Despawn()
        {
            if (_pool != null)
                _pool.Despawn(this);
        }

        private float CalcDuration()
        {
            float maxDuration = 0f;
          
            if (_particles != null)
            {
                foreach (var particle in _particles)
                {
                    if (!particle) continue;
                    var main = particle.main;
                    var duration = main.duration + main.startLifetime.constantMax;
                    if (duration > maxDuration) maxDuration = duration;
                }
            }

            if (_overrideDuration > 0f) maxDuration = _overrideDuration;
            return maxDuration;
        }
    }
}
