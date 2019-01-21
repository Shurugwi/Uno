using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using CoreAnimation;
using CoreGraphics;

namespace Uno.UI.Media
{
	partial class NativeRenderTransformAdapter
	{
		private static CATransform3D ToNative(Matrix3x2 matrix)
			=> new CATransform3D
			{
				m11 = (nfloat)matrix.M11,
				m12 = (nfloat)matrix.M12,
				m21 = (nfloat)matrix.M21,
				m22 = (nfloat)matrix.M22,
				m31 = (nfloat)matrix.M31,
				m32 = (nfloat)matrix.M32
			};

		partial void Apply(Matrix3x2 matrix, bool isSizeChanged)
		{
			// On iOS Transform are applied by default on the center on the view

			Owner.Layer.AnchorPoint = CGPoint.Empty;
			Owner.Layer.Transform = ToNative(matrix);
		}

		partial void Cleanup()
		{
			Owner.Layer.AnchorPoint = CGPoint.Empty;
			Owner.Layer.Transform = CATransform3D.Identity;
		}
	}
}
