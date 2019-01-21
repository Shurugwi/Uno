using Android.Graphics;
using System;
using System.Numerics;
using Windows.Foundation;

namespace Windows.UI.Xaml.Media
{
	/// <summary>
	/// Transform: Android part
	/// </summary>
	public partial class Transform
	{
		internal virtual Android.Graphics.Matrix ToNative(Android.Graphics.Matrix targetMatrix = null, Size size = new Size(), bool isBrush = false)
		{
			throw new NotImplementedException();
		}
	}
}
