using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SBaier.Astrominer
{
	public class TransformPositionAsteroidsCreator : AsteroidsCreator
	{
		[SerializeField]
		private List<Transform> _positions = new List<Transform>();

		protected override IEnumerable<Vector2> GetPositions()
		{
			return _positions.Select(p => (Vector2) p.position);
		}
	}
}
