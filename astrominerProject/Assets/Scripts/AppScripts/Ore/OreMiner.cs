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
		private bool Mining => !_asteroid.Exploited && !_oresPerSecond.IsEmpty();

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
				AddOres(Time.deltaTime);
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

			float ironPart = ExploitableOres.Iron.Amount / allOres;
			float ironPerSecond = factor * ironPart;

			float goldPart = ExploitableOres.Gold.Amount / allOres;
			float goldPerSecond = factor * goldPart;

			float platinumPart = ExploitableOres.Platinum.Amount / allOres;
			float platinumPerSecond = factor * platinumPart;

			_oresPerSecond = new Ores(ironPerSecond, goldPerSecond, platinumPerSecond);
		}

		private void SetEmpltyOresPerSecond()
		{
			_oresPerSecond = new Ores();
		}

		private void AddOres(float deltaTime)
		{
			float iron = _oresPerSecond.Iron.Amount * deltaTime;
			iron = Mathf.Min(iron, _asteroid.ExploitableOres.Iron.Amount);
			float gold = _oresPerSecond.Gold.Amount * deltaTime;
			gold = Mathf.Min(gold, _asteroid.ExploitableOres.Gold.Amount);
			float platinum = _oresPerSecond.Platinum.Amount * deltaTime;
			platinum = Mathf.Min(platinum, _asteroid.ExploitableOres.Platinum.Amount);
			_asteroid.MineOre(iron, gold, platinum);
		}
	}
}
