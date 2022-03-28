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

		public Player OwningPlayer { get; private set; }
		public event Action OnOwningPlayerChanged;
		public bool HasOwningPlayer => OwningPlayer != null;
		public ExploitMachine ExploitMachine { get; private set; }
		public event Action OnExploitMachineChanged;

		void Injectable.Inject(Resolver resolver)
		{
			_arguments = resolver.Resolve<Arguments>();
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

		public class Arguments
		{
			public int Quality { get; }
			public int Size { get; }
			public Color Color { get; }

			public Arguments(int quality,
				int size,
				Color color)
			{
				Quality = quality;
				Size = size;
				Color = color;
			}
		}
	}
}
