using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using CoreGraphics;

namespace Uno.UI.Media
{
	partial class NativeRenderTransformAdapter
	{
		private static CGAffineTransform ToNative(Matrix3x2 matrix)
			=> new CGAffineTransform(
				(nfloat)matrix.M11,
				(nfloat)matrix.M12,
				(nfloat)matrix.M21,
				(nfloat)matrix.M22,
				(nfloat)matrix.M31,
				(nfloat)matrix.M32
			);

		partial void Apply(Matrix3x2 matrix, bool isSizeChanged)
		{
			// On iOS Transform are applied by default on the center on the view

			Owner.Layer.AnchorPoint = CGPoint.Empty;
			Owner.Transform = ToNative(matrix);
		}

		partial void Cleanup()
		{
			Owner.Transform = CGAffineTransform.MakeIdentity();
		}
	}
}
