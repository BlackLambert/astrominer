using SBaier.DI;
using System;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class Asteroid : CosmicObject, Injectable
	{
		private float _epsilon = 0.0001f;

		[field: SerializeField]
		public Transform Base { get; private set; }
		[SerializeField]
		private Transform _image;

		private Arguments _arguments;

		public Player OwningPlayer { get; private set; }
		public Ores MinedOres { get; } = new Ores();
		public Ores TotalMinedOres { get; } = new Ores();
		public Ores ExploitableOres { get; private set; } = new Ores();
		public float MinedPercentage { get; private set; } = 0;
		public ExploitMachine ExploitMachine { get; private set; }

		public event Action OnOwningPlayerChanged;
		public bool HasOwningPlayer => OwningPlayer != null;
		public bool HasExploitMachine => ExploitMachine != null;
		public event Action OnExploitMachineChanged;
		public bool Exploited => ExploitableOres.GetTotal() <= _epsilon;
		public float OresPercentage => BodyMaterials.OresPercentage;
		public int Quality => _arguments.Quality;
		public int Size => _arguments.Size;
		public Color BaseColor => _arguments.Color;
		public Color ExploitedColorReduction => _arguments.ExploitedColorReduction;
		public Ores TotalExploitableOres => _arguments.TotalExploitableOres;
		public AsteroidBodyMaterials BodyMaterials => _arguments.AsteroidBodyMaterials;
		public float Value => (1 - MinedPercentage) * (Size + Quality);

		public event Action OnOreMined;
		public event Action OnOresCollected;
		public event Action OnExploited;
		
		private Vector3 _startScale; 

		private void Awake()
		{
			_startScale = _image.transform.localScale;
		}

		void Injectable.Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<Arguments>();
			ExploitableOres.Add(_arguments.TotalExploitableOres);
		}

		public void SetObjectSize(float size)
        {
			Vector3 scale = _startScale;
			_image.transform.localScale = new Vector3(size * scale.x, size * scale.y, size * scale.z);
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

		public ExploitMachine TakeExploitMachine()
		{
			if (ExploitMachine == null)
				throw new InvalidOperationException("No Exploit Machine to take");
			ExploitMachine machine = ExploitMachine;
			ExploitMachine = null;
			OnExploitMachineChanged?.Invoke();
			return machine;
		}

		public void SetName(string name)
        {
			Base.name = name;
		}

		public void MineOres(Ores oresDelta)
		{
			if (Exploited)
				throw new InvalidOperationException("You can not mine an exploited asteroid.");
			Ores minedOres = ExploitableOres.Request(oresDelta);
			MinedOres.Add(minedOres);
			TotalMinedOres.Add(minedOres);
			CalculateMinedPercentage();
			OnOreMined?.Invoke();
			if (Exploited)
				OnExploited?.Invoke();
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
			public Vector2 Position { get; }
			public Quaternion Rotation { get; }
			public int Quality { get; }
			public int Size { get; }
			public Color Color { get; }
			public AsteroidBodyMaterials AsteroidBodyMaterials { get; }
			public Color ExploitedColorReduction { get; }
			public Ores TotalExploitableOres => AsteroidBodyMaterials.Ores;

			public Arguments(
				Vector2 position,
				Quaternion rotation,
				int quality,
				int size,
				Color color, 
				AsteroidBodyMaterials asteroidBodyMaterials,
				Color exploitedColorReduction)
			{
				Position = position;
				Rotation = rotation;
				Quality = quality;
				Size = size;
				Color = color;
				AsteroidBodyMaterials = asteroidBodyMaterials;
				ExploitedColorReduction = exploitedColorReduction;
			}
		}
	}
}
