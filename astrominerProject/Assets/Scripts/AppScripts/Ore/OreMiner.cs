using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class OreMiner : MonoBehaviour, Injectable
	{
		private Asteroid _asteroid;

		private ExploitMachine ExploitMachine => _asteroid.ExploitMachine;
		private float BaseSpeed => _asteroid.BaseMiningSpeed;
		private Ores ExploitableOres => _asteroid.TotalExploitableOres;

		private Ores _oresPerSecond;
		private Ores _oresDelta = new Ores();
		private bool Mining => !_asteroid.Exploited && 
			!_oresPerSecond.IsEmpty() && 
			Time.deltaTime != 0;

		public void Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
		}

		private void Start()
		{
			CalculateOresPerSecond();
			_asteroid.OnExploitMachineChanged += CalculateOresPerSecond;
		}

		private void OnDestroy()
		{
			_asteroid.OnExploitMachineChanged -= CalculateOresPerSecond;
		}

		private void Update()
		{
			if(Mining)
				MineOres();
		}

		private void CalculateOresPerSecond()
		{
			if (!_asteroid.HasExploitMachine)
				SetEmpltyOresPerSecond();
			else
				CalculateMachineOreMiningPerSecond();
		}

		private void CalculateMachineOreMiningPerSecond()
		{
			float factor = ExploitMachine.Level * BaseSpeed;
			float allOres = ExploitableOres.GetTotal();
			Ores result = new Ores();
			foreach (OreType oreType in ExploitableOres.OreTypes)
				result.Add(oreType, factor * (ExploitableOres[oreType].Amount / allOres));
			_oresPerSecond = result;
		}

		private void SetEmpltyOresPerSecond()
		{
			_oresPerSecond = new Ores();
		}

		private void MineOres()
		{
			CalculateOresDelta();
			_asteroid.MineOres(_oresDelta);
		}

		private void CalculateOresDelta()
		{
			foreach (OreType type in _oresPerSecond.OreTypes)
				CalculateOreDelta(type);
		}

		private void CalculateOreDelta(OreType type)
		{
			float ore = _oresPerSecond[type].Amount * Time.deltaTime;
			ore = Mathf.Min(ore, _asteroid.ExploitableOres[type].Amount);
			_oresDelta[type].Set(ore);
		}
	}
}
