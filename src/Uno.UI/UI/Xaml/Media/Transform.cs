using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Foundation;
using Uno.UI.Controls;

#if __ANDROID__
using _View = Android.Views.View;
#elif __IOS__
using _View = UIKit.UIView;
#elif __MACOS__
using _View = AppKit.NSView;
#elif __WASM__
using _View = Windows.UI.Xaml.UIElement;
#else
using _View = System.Object;
#endif

namespace Windows.UI.Xaml.Media
{
	/// <summary>
	/// Transform :  Based on the WinRT Transform
	/// 
	/// https://msdn.microsoft.com/en-us/library/system.windows.media.transform(v=vs.110).aspx
	/// </summary>
	public abstract partial class Transform : GeneralTransform
	{
		protected static PropertyChangedCallback NotifyChangedCallback { get; } = (snd, args) =>
		{
			// Don't update the internal value if the value is being animated.
			// The value is being animated by the platform itself.

			if (snd is Transform transform
				&& !(args.NewPrecedence == DependencyPropertyValuePrecedences.Animations && args.BypassesPropagation))
			{
				transform.NotifyChanged();
			}
		};

		internal event EventHandler Changed;

		protected void NotifyChanged()
			=> Changed?.Invoke(this, EventArgs.Empty);

		/// <summary>
		/// Converts the transform to a standard transform matrix
		/// </summary>
		/// <param name="relativeOrigin">The origin of the transform relative to the <paramref name="viewSize"/>.</param>
		/// <param name="viewSize">The size of the view to transform, in virtual pixels</param>
		/// <returns>An affine matrix of the transformation</returns>
		internal Matrix3x2 ToMatrix(Point relativeOrigin, Size viewSize)
			=> ToMatrix(new Point(relativeOrigin.X * viewSize.Width, relativeOrigin.Y * viewSize.Height));

		/// <summary>
		/// Converts the transform to a standard transform matrix
		/// </summary>
		/// <param name="absoluteOrigin">The absolute origin of the transform, in virtual pixels.</param>
		/// <returns>An affine matrix of the transformation</returns>
		internal abstract Matrix3x2 ToMatrix(Point absoluteOrigin);

		// Currently we support only one view par transform.
		// But we can declare a Transform as a static resource and use it on multiple views.
		// Note: This is now used only for animations
		internal _View View { get; set; }
	}
}


