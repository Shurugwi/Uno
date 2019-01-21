using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using System.Drawing;
using System.Numerics;
using Uno.UI;
using CoreAnimation;

#if __IOS__
using _View = UIKit.UIView;
#elif __MACOS__
using _View = AppKit.NSView;
#endif

namespace Windows.UI.Xaml.Media
{
	/// <summary>
	/// Transform: iOS part
	/// </summary>
	public abstract partial class Transform
	{
#if __IOS__
		internal static CGAffineTransform ToNative(Matrix3x2 matrix)
			=> new CGAffineTransform(
				(nfloat)matrix.M11,
				(nfloat)matrix.M12,
				(nfloat)matrix.M21,
				(nfloat)matrix.M22,
				(nfloat)matrix.M31,
				(nfloat)matrix.M32
			);

		internal CGAffineTransform ToNative(CGSize size)
			=> ToNative(ToMatrix(new Foundation.Point(0, 0)));

#elif __MACOS__
		internal static CATransform3D ToNative(Matrix3x2 matrix)
			=> new CATransform3D
			{ 
				m11 = (nfloat)matrix.M11,
				m12 = (nfloat)matrix.M12,
				m21 = (nfloat)matrix.M21,
				m22 = (nfloat)matrix.M22,
				m31 = (nfloat)matrix.M31,
				m32 = (nfloat)matrix.M32
			};

		internal CATransform3D ToNative(CGSize size)
			=> ToNative(ToMatrix(new Foundation.Point(0, 0)));
#endif
	}
}

