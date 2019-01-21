using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Uno.Extensions;

namespace Uno.UI.Media
{
	partial class NativeRenderTransformAdapter
	{
		partial void Apply(Matrix3x2 m, bool isSizeChanged)
		{
			var matrix = $"matrix({m.M11.ToStringInvariant()},{m.M12.ToStringInvariant()},{m.M21.ToStringInvariant()},{m.M22.ToStringInvariant()},{m.M31.ToStringInvariant()},{m.M32.ToStringInvariant()})";
			Owner.SetStyle("transform-origin", "0 0"); // By default transform are centered on the view
			Owner.SetStyle("transform", matrix);
		}

		partial void Cleanup()
		{
			Owner.ResetStyle("transform");
		}
	}
}
