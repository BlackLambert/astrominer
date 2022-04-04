using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class OreMiner : MonoBehaviour, Injectable
	{
		private Asteroid _asteroid;
		private MiningSettings _settings;

		private ExploitMachine ExploitMachine => _asteroid.ExploitMachine;
		private float BaseSecondsTillExploit => _settings.BaseSecondsTillExploit;
		private float QualityMineSpeedAddition => _settings.QualityMineSpeedAddition;
		private Ores TotalExploitableOres => _asteroid.TotalExploitableOres;
		private float Quality => _asteroid.Quality;

		private Ores _oresPerSecond;
		private Ores _oresDelta = new Ores();
		private bool Mining => !_asteroid.Exploited && 
			!_oresPerSecond.IsEmpty() && 
			Time.deltaTime != 0;

		public void Inject(Resolver resolver)
		{
			_asteroid = resolver.Resolve<Asteroid>();
			_settings = resolver.Resolve<MiningSettings>();
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
			float allExploitableOreAmount = TotalExploitableOres.GetTotal();
			float baseDelta = allExploitableOreAmount / BaseSecondsTillExploit;
			float qualityDelta = baseDelta * (1 + Quality * QualityMineSpeedAddition);
			float machineDelta = qualityDelta * ExploitMachine.Power;
			Ores result = new Ores();
			foreach (OreType oreType in TotalExploitableOres.OreTypes)
				result.Add(oreType, machineDelta * (TotalExploitableOres[oreType].Amount / allExploitableOreAmount));
			_oresPerSecond = result;
			Debug.Log($"CalculateMachineOreMiningPerSecond - TotalExploitableOres: {allExploitableOreAmount} | OresPerSecond {_oresPerSecond} | machineDelta {machineDelta}");
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
