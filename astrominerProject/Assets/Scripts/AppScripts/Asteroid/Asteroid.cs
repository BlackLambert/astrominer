using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class Asteroid : CosmicObject, Injectable, FlyTarget
	{
		[field: SerializeField]
		public Transform Base { get; private set; }
		[SerializeField]
		private Transform _image;

		Arguments _arguments;

		public int Quality => _arguments.Quality;
		public int Size => _arguments.Size;
		public Color BaseColor => _arguments.Color;
		public Ores TotalExploitableOres => _arguments.TotalExploitableOres;
		public float BaseMiningSpeed => _arguments.BaseMiningSpeed;

		public Player OwningPlayer { get; private set; }
		public event Action OnOwningPlayerChanged;
		public bool HasOwningPlayer => OwningPlayer != null;

		public ExploitMachine ExploitMachine { get; private set; }
		public bool HasExploitMachine => ExploitMachine != null;
		public event Action OnExploitMachineChanged;
		public Ores MinedOres { get; } = new Ores();
		public Ores TotalMinedOres { get; } = new Ores();
		public Ores ExploitableOres { get; private set; } = new Ores();
		public float MinedPercentage { get; private set; } = 0;
		public bool Exploited => MinedPercentage >= 1;
		public event Action OnOreMined;
		public event Action OnOresCollected;

		void Injectable.Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<Arguments>();
			ExploitableOres.Add(_arguments.TotalExploitableOres);
		}

		public void SetObjectSize(float size)
        {
			_image.transform.localScale = new Vector3(size, size, size);
		}

		public void SetPosition(Vector2 position)
        {
			Base.position = position;
		}

		public void SetRotation(Quaternion rotation)
		{
			_image.transform.rotation = rotation;
		}

		public void SetOwningPlayer(Player player)
		{
			OwningPlayer = player;
			OnOwningPlayerChanged?.Invoke();
		}

		public void PlaceExploitMachine(ExploitMachine machine)
		{
			if (ExploitMachine != null)
				throw new InvalidOperationException("Remove the current Exploit Machine before placing another");
			if (machine == null)
				throw new ArgumentNullException();
			ExploitMachine = machine;
			OnExploitMachineChanged?.Invoke();
		}

		public void TakeExploitMachine()
		{
			if (ExploitMachine == null)
				throw new InvalidOperationException("No Exploit Machine to take");
			ExploitMachine machine = ExploitMachine;
			ExploitMachine = null;
			OnExploitMachineChanged?.Invoke();
		}

		public void SetName(string name)
        {
			Base.name = name;
		}

		public void MineOres(Ores oresDelta)
		{
			if (MinedPercentage >= 1)
				throw new InvalidOperationException("You can not mine an exploited asteroid.");
			Ores minedOres = ExploitableOres.Request(oresDelta);
			MinedOres.Add(minedOres);
			TotalMinedOres.Add(minedOres);
			CalculateMinedPercentage();
			OnOreMined?.Invoke();
		}

		private void CalculateMinedPercentage()
		{
			float result = 1;
			foreach (OreType type in TotalMinedOres.OreTypes)
				result = Mathf.Min(result, TotalMinedOres[type].Amount / TotalExploitableOres[type].Amount);
			MinedPercentage = result;
		}

		public Ores Collect()
		{
			Ores ores = MinedOres.RequestAll();
			OnOresCollected?.Invoke();
			return ores;
		}

		public class Arguments
		{
			public int Quality { get; }
			public int Size { get; }
			public Color Color { get; }
			public Ores TotalExploitableOres { get; }
			public float BaseMiningSpeed { get; internal set; }

			public Arguments(int quality,
				int size,
				Color color, 
				Ores exploitableOres,
				float baseMiningSpeed)
			{
				Quality = quality;
				Size = size;
				Color = color;
				TotalExploitableOres = exploitableOres;
				BaseMiningSpeed = baseMiningSpeed;
			}
		}
	}
}
