using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UniRx
{
	public static class UnityUIExtensions
	{
		public static IDisposable SubscribeToText(this IObservable<string> source, TextMeshProUGUI text)
		{
			return source.SubscribeWithState(text, (x, t) => t.text = x);
		}

		public static IDisposable SubscribeToText<T>(this IObservable<T> source, TextMeshProUGUI text)
		{
			return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
		}

		public static IDisposable SubscribeToTextM<T>(this IObservable<T> source, TextMeshPro text)
		{
			return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
		}
	}
}